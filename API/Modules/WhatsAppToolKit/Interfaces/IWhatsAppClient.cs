using OpenQA.Selenium;

namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppClient : IDisposable
{
    byte[] TakeScreenshot();
    bool ConversationIsOpen();
    void NavigateTo(string link);
    void Open(string? profile = null);
    Task<T> GetElementAsync<T>(By by, int timeout = 5, int sleep = 1);
}
