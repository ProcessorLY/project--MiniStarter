using System.IO;

namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppMessanger : IDisposable
{
    void OpenClientConversation(string phoneNumber);
    void SendMessage(string message);
    void SendFile(string path);
    void SendFile(Stream file);
    void SendVoice(Stream voice);
    void SendPdf(string path);
    void SendPdf(Stream pdfFile);
}
