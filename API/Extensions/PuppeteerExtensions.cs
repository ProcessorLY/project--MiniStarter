using PuppeteerSharp;
using System.Threading;

namespace API.Extensions;
public static class PuppeteerExtensions
{
    private static string _executablePath = "";
    public static async Task PreparePuppeteerAsync(this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment hostingEnvironment)
    {
        var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, @"\puppeteer");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) downloadPath = downloadPath.Replace(@"\", "/");

        var browserOptions = new BrowserFetcherOptions
        {
            Path = downloadPath,
            //Platform = Platform.Win32,
            //Browser = SupportedBrowser.Chromium,
        };
        var browserFetcher = new BrowserFetcher(browserOptions);
        await Loading(browserFetcher);

        //var doanlaodble = await browserFetcher.DownloadAsync();
        //_executablePath = doanlaodble.GetExecutablePath();
        //Console.WriteLine(_executablePath);
    }

    private static async Task Loading(BrowserFetcher browserFetcher)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        //Task.Run(async () =>
        //{
        //    int indx = -1;
        //    var leetrs = new string[] { "\t\t\\", "\t\t|", "\t\t/" };
        //    while (true)
        //    {
        //        await Task.Delay(100);
        //        if (indx + 1 >= 3) indx = 0;
        //        else indx++;

        //        Console.Clear();
        //        Console.Write(leetrs[indx]);
        //    }
        //}, cancellationTokenSource.Token);

        var doanlaodble = await browserFetcher.DownloadAsync();
        cancellationTokenSource.Cancel();

        _executablePath = doanlaodble.GetExecutablePath();

    }

    public static string ExecutablePath => _executablePath;
}