using API.Modules.WhatsAppToolKit.Interfaces;

namespace API.Modules.WhatsAppToolKit;

public class WhatsAppSubscriper
{
    public static Dictionary<string, IWhatsAppMessangerAsync> WhatsAppSubscripers { get; private set; } = new Dictionary<string, IWhatsAppMessangerAsync>();

    public static void Remove(string token)
    {
        WhatsAppSubscripers?.Remove(token);
    }

    public static void Add(string token, IWhatsAppMessangerAsync whatsApp)
    {
        WhatsAppSubscripers ??= new Dictionary<string, IWhatsAppMessangerAsync>();
        WhatsAppSubscripers.Add(token, whatsApp);
    }

    public static async void Refresh()
    {
        if (WhatsAppSubscripers is null || !WhatsAppSubscripers.Any()) return;

        for (int i = WhatsAppSubscripers.Count - 1; i >= 0; i--)
        {
            var it = WhatsAppSubscripers.ElementAt(i);
            var a = it.Value;
            try
            {
                //await a.OpenClientConversationAsync("0");
                var stats = await a.IsAvaliableAsync();
                if (!stats) WhatsAppSubscripers.Remove(it.Key);
            }
            catch (Exception)
            {
                WhatsAppSubscripers.Remove(it.Key);
            }
        }
    }


}
