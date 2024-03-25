using API.Modules.WhatsAppToolKit.Interfaces;
using OpenQA.Selenium;

namespace API.Modules.WhatsAppToolKit.Clients.PlaywrightBased;


public class PlaywrightWhatsAppClient : IWhatsAppClient
{

    private WebDriver? _driver;
    private readonly Microsoft.Extensions.Logging.ILogger logger;

    public PlaywrightWhatsAppClient(ILogger<PlaywrightWhatsAppClient> logger)
    {
        this.logger = logger;
    }


    public bool ConversationIsOpen()
    {
        throw new NotImplementedException();
    }

    public void NavigateTo(string link)
    {
        throw new NotImplementedException();
    }

    public Byte[] TakeScreenshot()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ConversationIsOpenAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IWebElement?> GetElementAsync(By by, int timeout = 5, int sleep = 1)
    {
        throw new NotImplementedException();
    }

    public Task NavigateToAsync(string link)
    {
        throw new NotImplementedException();
    }

    public void Open(string? profile = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> WaitUntilElementAppearsAsync(By by)
    {
        throw new NotImplementedException();
    }
}
