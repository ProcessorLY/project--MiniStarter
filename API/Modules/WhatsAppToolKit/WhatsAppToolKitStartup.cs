using API.Modules.WhatsAppToolKit.Clients.PuppeteerBased;
using API.Modules.WhatsAppToolKit.Clients.SeleniumBased;
using API.Modules.WhatsAppToolKit.Interfaces;
using PuppeteerSharp;

namespace API.Modules.WhatsAppToolKit;

public static class WhatsAppToolKitStartup
{
    public static async Task WhatsAppToolKitInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWhatsAppMessangerAsync, WhatsAppMessanger>();
        await services.UsePuppeteer();
    }


    private static async Task UsePuppeteer(this IServiceCollection services)
    {
        await services.DownlaodNeededPuppeteer();
        services.AddScoped<IWhatsAppClient, PuppeteerWhatsAppClient>();
    }
    private static Task UseSelenium(this IServiceCollection services)
    {
        services.AddScoped<ISeleniumClientAsync, SeleniumWhatsAppClient>();
        return Task.CompletedTask;
    }
    private static Task UsePlaywright(this IServiceCollection services)
    {
        services.AddScoped<IWhatsAppClient, SeleniumWhatsAppClient>();
        return Task.CompletedTask;
    }


    private static async Task DownlaodNeededPuppeteer(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        //var logger = provider.GetRequiredService<Serilog.ILogger>();
        var webConfig = provider.GetRequiredService<IWebHostEnvironment>();
        var downloadDir = Path.Combine(webConfig.ContentRootPath, "puppeteer_2");
        var option = new BrowserFetcherOptions()
        {
            Path = downloadDir,
            Platform = Platform.Win64,
            Browser = SupportedBrowser.Chromium,
        };
        using var browserFetcher = new BrowserFetcher(option);
        //Log("downlaoding files ...");
        await browserFetcher.DownloadAsync();
    }
}
