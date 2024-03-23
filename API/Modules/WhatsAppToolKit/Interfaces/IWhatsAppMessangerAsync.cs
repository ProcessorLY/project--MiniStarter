namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppMessangerAsync
{
    Task OpenClientConversationAsync(string phoneNumber);
    Task SendMessageAsync(string message);
    Task SendFileAsync(string path);
    Task SendFileAsync(Stream file);
    Task SendVoiceAsync(Stream voice);
    Task SendPdfAsync(string path);
    Task SendPdfAsync(Stream pdfFile);
}
