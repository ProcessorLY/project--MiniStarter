using API.Modules.WhatsAppToolKit.Contants;
using API.Modules.WhatsAppToolKit.Exceptions;
using API.Modules.WhatsAppToolKit.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace API.Modules.WhatsAppToolKit;

public class WhatsAppMessanger : IWhatsAppMessangerAsync, IDisposable
{
    private string? _userPhone;
    private bool _userSelected = false;
    private IWhatsAppClientAsync _client;
    private readonly Microsoft.Extensions.Logging.ILogger logger;

    public WhatsAppMessanger(IWhatsAppClientAsync whatsAppMessanger, Microsoft.Extensions.Logging.ILogger<WhatsAppMessanger> logger)
    {
        this.logger = logger;
        _client = whatsAppMessanger;
    }


    private async Task<bool> IsChatOpened()
    {
        var element = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT));
        if (element is null)
        {
            if (_userPhone is null)
            {
                return false;
            }
            await OpenClientConversationAsync(_userPhone);
            return true;
        }
        else return true;
    }
    private async Task<bool> IsLoggedIn()
    {

        var qrElement = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_QRCODE));
        if (qrElement is not null) return false;

        var searchElement = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT_SEARCH));
        if (searchElement is null)
        {
            return false;
        }
        else
            return true;
    }

    private async Task<Image?> Login()
    {
        var qrCodeElement = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_QRCODE));
        if (qrCodeElement is null) return null;

        var elSize = qrCodeElement.Size;
        var elLocation = qrCodeElement.Location;
        //elLocation.

        // Take a screenshot of the QR code
        var screenshot = _client.TakeScreenshot();
        Image? image = Image.Load(screenshot.AsByteArray);

        // Convert the screenshot to an image
        /*
        using (var ms = new MemoryStream(screenshot.AsByteArray))
        {
            image = await Image.LoadAsync(ms);
            // Display the QR code image in a dialog
            //using (var dialog = new Form())
            //{
            //    dialog.Text = "WhatsApp QR Code";
            //    dialog.ClientSize = new Size(image.Width, image.Height);
            //    dialog.BackgroundImage = image;
            //    dialog.ShowDialog();
            //}
        }
        */

        image = image.Clone(x =>
        {
            x.Crop(new Rectangle(elLocation.X, elLocation.Y, elSize.Width, elSize.Height));
        });
        return image;
    }

    public async Task OpenClientConversationAsync(string phoneNumber)
    {
        await _client.NavigateToAsync("https://web.whatsapp.com/");

        int maxTries = 3;
        _userSelected = false;
        _userPhone = phoneNumber;
        // Check if the phone number has WhatsApp installed.
        for (int i = 0; i <= maxTries; i++)
        {
            if ((await IsLoggedIn())) break;
            else
            {
                var image = await Login();
                if (image is null) throw new Exception("image is null");
                throw new QRCodeAuthenticationNeededException(image);
            }
        }
        // Open the conversation.
        await _client.NavigateToAsync($"https://web.whatsapp.com/send?phone={phoneNumber}");
        _userSelected = true;
    }

    #region 

    public async Task SendMessageAsync(string message)
    {
        // Check if the conversation is open.
        var isOpend = await IsChatOpened();
        if (!isOpend)
        {
            // Send the message.
            var element = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT)) ?? throw new Exception();
            element.SendKeys(message);
            element.SendKeys(Keys.Enter);
        }
        else
        {
            return;
        }
    }

    public Task SendFileAsync(string path)
    {
        //// Check if the conversation is open.
        //if (!_client.ConversationIsOpen())
        //{
        //    throw new ConversationNotOpenException();
        //}

        //// Send the file.
        //_client.SendFile(path);
        return Task.CompletedTask;
    }

    public Task SendFileAsync(Stream file)
    {
        //// Check if the conversation is open.
        //if (!_client.ConversationIsOpen())
        //{
        //    throw new ConversationNotOpenException();
        //}

        //// Send the file.
        //_client.SendFile(file);
        return Task.CompletedTask;
    }

    public Task SendVoiceAsync(Stream voice)
    {
        //// Check if the conversation is open.
        //if (!_client.ConversationIsOpen())
        //{
        //    throw new ConversationNotOpenException();
        //}

        //// Send the voice message.
        //_client.SendVoice(voice);
        return Task.CompletedTask;
    }

    public Task SendPdfAsync(string path)
    {
        //// Check if the conversation is open.
        //if (!_client.ConversationIsOpen())
        //{
        //    throw new ConversationNotOpenException();
        //}

        //// Send the PDF file.
        //_client.SendPdf(path);
        return Task.CompletedTask;
    }

    public Task SendPdfAsync(Stream pdfFile)
    {
        //// Check if the conversation is open.
        //if (!_client.ConversationIsOpen())
        //{
        //    throw new ConversationNotOpenException();
        //}

        //// Send the PDF file.
        //_client.SendPdf(pdfFile);
        return Task.CompletedTask;
    }

    #endregion

    public void Dispose()
    {
        _client.Dispose();
    }
}