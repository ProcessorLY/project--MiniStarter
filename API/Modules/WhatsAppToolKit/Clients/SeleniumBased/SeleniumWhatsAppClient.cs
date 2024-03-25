using API.Modules.WhatsAppToolKit.Contants;
using API.Modules.WhatsAppToolKit.Exceptions;
using API.Modules.WhatsAppToolKit.Extentions;
using API.Modules.WhatsAppToolKit.Extentions.SeleniumBased;
using API.Modules.WhatsAppToolKit.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace API.Modules.WhatsAppToolKit.Clients.SeleniumBased;

public class SeleniumWhatsAppClient : ISeleniumClientAsync
{

    private WebDriver? _driver;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    public SeleniumWhatsAppClient(ILogger<SeleniumWhatsAppClient> logger)
    {
        _logger = logger;
    }


    private void Log(string m) => _logger.LogInformation(m);

    public void Open(string? profile = null)
    {
        _logger.LogDebug("SeleniumWhatsAppClient starts ...");
        var options = CreateChromeOptions(userProfile: profile);
        _driver = new EdgeDriver(options);
    }

    private string GetDefultUser() => "Profile 4"; //"Default";
    private string GetDefultUserDir()
        => Path.Combine(
            Environment.GetEnvironmentVariable("userprofile")!,
            "AppData", "Local", "Google", "Chrome", "User Data");

    private EdgeOptions CreateChromeOptions(string? userDataPathSrc = null, string? userProfile = null)
    {
        userProfile = userProfile is null ? GetDefultUser() : "Profile " + userProfile;
        userDataPathSrc ??= GetDefultUserDir();
        // https://peter.sh/experiments/chromium-command-line-switches/
        var options = new EdgeOptions()
        {
            EnableDownloads = false,
        };
        options.AddArguments(
            //$"custom-user-data-dir={userProfile}",
            //"--allow-profiles-outside-user-dir"
            $"--user-data-dir={userDataPathSrc}"
            , $"--profile-directory={userProfile}"

            //, "--disable-plugins"
            //, "--disable-extensions"
            //, "--disable-default-apps"
            //, "--disable-background-mode"
            //, "--disable-background-networking"

            //, "--disable-gpu"
            //, "--disable-logging"
            //, "--disable-remote-fonts"
            //, "--disable-smooth-scrolling"

            //, "--disable-gl-drawing-for-tests"

            //, "--no-sandbox"
            //, "--disable-dev-shm-usage"
            , "--disable-permissions-api"
            , "--headless"
        //   ,"no-startup-window"
        //  ,"start-fullscreen"
        );
        return options;
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

    private async Task<IWebElement?> GetElementAsync(By by, int timeout = 0, int sleep = 0)
    {
        return await Task.Run(() =>
        {
            return _driver.WaitUntilElementPresent(by, timeout, sleep);
        });
    }

    public async Task<bool> WaitUntilElementAppearsAsync(By by)
    {
        return await Task.Run(() =>
        {
            return _driver.WaituntilElementPresent(by);
        });
    }

    public byte[] TakeScreenshot()
    {
        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
        return screenshot.AsByteArray;
    }

    public void Dispose()
    {
        //_driver.Dispose();
    }

    public Task<T> GetElementAsync<T>(By by, int timeout = 5, int sleep = 1)
    {
        throw new NotImplementedException();
    }



    #region Helpers


    private async Task AddUserFlag(string phone)
    {
        //var js = (IJavaScriptExecutor)_driver!;
        if (_driver is null) throw new Exception();
        //var __id__userflag = "user-flag";

        String code =
            "let body = document.body;" +
            "let element = document.createElement('div');" +
            "" +
            "element.setAttribute('id', 'user-flag')" +
            $"element.setAttribute('data-phone', {phone})" +
            "" +
            "body.append(element);";

        var flagEl = await GetElementAsync(By.Id("user-flag"));
        if (flagEl is null)
        {
            _driver.ExecuteScript(code);
        }
        else
        {
            _driver.ExecuteScript("document.querySelector('#user-flag').setAttribute('data-phone', '{0}')", phone);
        }
    }
    private async Task<string?> GetCurrentUser()
    {
        var flagEl = await GetElementAsync(By.Id("user-flag"));
        if (flagEl is null)
        {
            return null;
        }
        else
        {
            return flagEl.GetAttribute("data-phone");
        }
    }
    private async Task<bool?> IsCurrentUser(string phone)
    {
        var curr = await GetCurrentUser();
        return curr.Equals(phone);
    }

    private async Task ThrowQrNeededException()
    {
        var qrElement = await GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_QRCODE));
        if (qrElement is null)
        {
            return;
        }
        var elSize = qrElement.Size;
        var elLocation = qrElement.Location;

        var screenshot = TakeScreenshot();

        Image? image = Image.Load(screenshot);
        image = image.Clone(x =>
        {
            x.Crop(new Rectangle(elLocation.X, elLocation.Y, elSize.Width, elSize.Height));
        });

        if (image is null) throw new Exception("image is null");
        throw new QRCodeAuthenticationNeededException(image);
    }

    #endregion

    #region Functions

    //  IsLoggedIn
    //  SendMessageAsync
    //  CheckDeliveryStatusAsync
    //  OpenClientConversationAsync
    //  .

    public async Task<bool> IsLoggedIn()
    {
        var contactListElement = await GetElementAsync(By.Id("side"), 1);
        if (contactListElement is not null) return true;
        //else return false;

        var qrElement = await GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_QRCODE));
        if (qrElement is not null)
        {
            await ThrowQrNeededException();
        }
        return false;

        /*
        var qrElement = await GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_QRCODE));
        if (qrElement is not null) return false;
        var searchElement = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT_SEARCH));
        if (searchElement is null) return false;
        else return true;
        return false;
        */
    }

    public async Task<bool> IsCurrentUserChat(string phone)
    {
        return (await IsCurrentUser(phone)) ?? false;
    }

    public async Task OpenClientChatAsync(string phoneNumber)
    {
        if (_driver is null) throw new Exception();

        Log("Open conversation with " + phoneNumber);
        await NavigateToAsync($"https://web.whatsapp.com/send?phone={phoneNumber}");
        await AddUserFlag(phoneNumber);

    }

    public async Task<Guid?> SendMessageAsync(string message)
    {

        var element = await GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT)) ?? throw new Exception();
        element.SendKeys(message);
        element.SendKeys(Keys.Enter);
        var lastMessageSent = await GetElementAsync(By.CssSelector("[role=\"application\"]>div:last-of-type"));
        var messageId = Guid.NewGuid();
        SetAttribute(lastMessageSent!, "data-messageid", messageId.ToString());
        return messageId;
    }
    public async Task<bool> CheckDeliveryStatusAsync(Guid messageId)
    {
        var messageElement = await GetElementAsync(By.CssSelector($"[data-messageid={messageId}]"));
        if (messageElement is null) return false;
        var deliveryStatus = messageElement.FindElement(By.CssSelector("span[aria-label=\" Delivered \"]"));
        return deliveryStatus is not null;
    }


    #endregion

    #region Utilties

    private void SetAttribute(IWebElement element, string name, string value)
    {
        if (_driver is null) throw new Exception();

        _driver.ExecuteScript("{0}.setAttribute({1}, {2});", element, name, value);
    }

    #endregion

}
