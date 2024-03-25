using OpenQA.Selenium;

namespace API.Modules.WhatsAppToolKit.Interfaces;

public interface ISeleniumClientAsync : IWhatsAppClient
{
    Task NavigateToAsync(string link);
    Task<bool> ConversationIsOpenAsync();
    Task<bool> WaitUntilElementAppearsAsync(By by);
    Task OpenClientChatAsync(string phoneNumber);
    Task<bool> IsLoggedIn();
    Task<bool> IsCurrentUserChat(string phone);
    //Task<IWebElement?> GetElementAsync(By by, int timeout = 5, int sleep = 1);
}