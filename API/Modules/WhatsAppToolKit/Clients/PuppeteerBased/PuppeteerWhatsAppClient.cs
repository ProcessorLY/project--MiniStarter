using API.Modules.WhatsAppToolKit.Interfaces;
using Microsoft.Playwright;
using OpenQA.Selenium;
using PuppeteerSharp;
using IBrowser = PuppeteerSharp.IBrowser;

namespace API.Modules.WhatsAppToolKit.Clients.PuppeteerBased;

public interface IPuppeteerWhatsAppClient : IWhatsAppClient
{

}

public class PuppeteerWhatsAppClient : IPuppeteerWhatsAppClient
{
    private bool _isReady;
    private IBrowser? _browser;
    private readonly IServiceProvider _serviceProvider;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    public PuppeteerWhatsAppClient(ILogger<PuppeteerWhatsAppClient> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        //Task.Run(DownlaodNeeded);
    }

    private void Log(string message) => _logger.LogInformation(message);

    public bool ConversationIsOpen()
    {
        throw new NotImplementedException();
    }

    public void NavigateTo(string link)
    {
        throw new NotImplementedException();
    }

    public byte[] TakeScreenshot()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        if (_browser is not null)
        {
            if (_browser.IsConnected) _browser.Disconnect();
            _browser.Dispose();
        }
    }

    public void Open(string? profile = null)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetElementAsync<T>(By by, int timeout = 5, int sleep = 1)
    {
        throw new NotImplementedException();
    }
}
