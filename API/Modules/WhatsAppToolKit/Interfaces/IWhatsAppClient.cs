using OpenQA.Selenium;

namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppClient : IDisposable
{
    bool ConversationIsOpen();
    void NavigateTo(string link);
    Screenshot TakeScreenshot();
}
