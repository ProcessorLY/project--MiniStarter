namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppMessangerAsync : IAsyncDisposable
{
    Task OpenClientConversationAsync(string phoneNumber);
    Task<bool> SendMessageAsync(string message);
    Task<bool> IsAvaliableAsync();
    Task SendFileAsync(string path);
    Task SendFileAsync(Stream file);
    Task SendVoiceAsync(Stream voice);
    Task SendPdfAsync(string path);
    Task SendPdfAsync(Stream pdfFile);
    void Open(string? profile = null);
}
