using OpenQA.Selenium;

namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface IWhatsAppClientAsync : IWhatsAppClient
{
    Task<bool> ConversationIsOpenAsync();
    Task<IWebElement?> GetElementAsync(By by, int timeout = 5, int sleep = 1);
    Task NavigateToAsync(string link);
}