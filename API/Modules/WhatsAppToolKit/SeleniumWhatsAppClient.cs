using API.Modules.WhatsAppToolKit.Extentions;
using API.Modules.WhatsAppToolKit.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Modules.WhatsAppToolKit;

public class SeleniumWhatsAppClient : IWhatsAppClient, IWhatsAppClientAsync
{

    private WebDriver _driver;
    private readonly Microsoft.Extensions.Logging.ILogger logger;

    public SeleniumWhatsAppClient(ILogger<SeleniumWhatsAppClient> logger)
    {
        logger.LogDebug("SeleniumWhatsAppClient starts ...");
        _driver = new ChromeDriver(new ChromeOptions()
        {

        });
        this.logger = logger;
    }


    public void NavigateTo(string link)
    {
        _driver.Navigate().GoToUrl(link);
        _driver.WaitUntilPageComplate();
    }
    public async Task NavigateToAsync(string link)
    {
        await Task.Run(() =>
        {
            NavigateTo(link);
        });
    }


    public bool ConversationIsOpen()
    {
        return false;
    }
    public async Task<bool> ConversationIsOpenAsync()
        => await Task.Run(ConversationIsOpen);

    public async Task<IWebElement?> GetElementAsync(By by, int timeout = 0, int sleep = 0)
    {
        return await Task.Run(() =>
        {
            return _driver.WaitUntilElementPresent(by, timeout, sleep);
        });
    }

    public Screenshot TakeScreenshot()
    {
        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
        return screenshot;
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}
