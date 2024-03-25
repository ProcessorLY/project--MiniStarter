using API.Modules.WhatsAppToolKit.Contants;
using API.Modules.WhatsAppToolKit.Exceptions;
using API.Modules.WhatsAppToolKit.Interfaces;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace API.Modules.WhatsAppToolKit;

public class WhatsAppMessanger : /*IWhatsAppMessanger,*/ IWhatsAppMessangerAsync
{
    private string? _userPhone;
    private bool _userSelected = false;
    private ISeleniumClientAsync _client;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    public WhatsAppMessanger(ISeleniumClientAsync whatsAppMessanger, Microsoft.Extensions.Logging.ILogger<WhatsAppMessanger> logger)
    {
        _logger = logger;
        _client = whatsAppMessanger;
    }

    public void Open(string? profile = null)
    {
        _client.Open(profile);
    }

    private void Log(string m)
    {
        _logger.LogInformation(m);
    }

    private async Task<bool> IsChatOpened()
    {
        Log("Check chat status");
        var element = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT));
        if (element is null)
        {
            Log("MESSAGE_INPUT not found");
            if (_userPhone is null)
            {
                Log("_userPhone not found");
                return false;
            }
            Log("Reopen conversation");
            await OpenClientConversationAsync(_userPhone);
            return true;
        }
        else return true;
    }

    public async Task OpenClientConversationAsync(string phoneNumber)
    {
        Log("Open conversation with " + phoneNumber);
        var isLoggedIn = await _client.IsLoggedIn();
        if (isLoggedIn)
        {
            var isThiscurrentUserChat = await _client.IsCurrentUserChat(phoneNumber);
            if (!isThiscurrentUserChat)
                await _client.OpenClientChatAsync(phoneNumber);
            _userSelected = true;
            _userPhone = phoneNumber;
        }
        else
        {

        }
        /*
        int maxTries = 3;
        _userSelected = false;
        _userPhone = phoneNumber;
        // Check if the phone number has WhatsApp installed.
        for (int i = 0; i <= maxTries; i++)
        {
            if ((await _client.IsLoggedIn()))
            {
                Log("Login status is TRUE");
                _userSelected = true;
                break;
            }
            else
            {
                var image = await Login();
                if (image is null) throw new Exception("image is null");
                throw new QRCodeAuthenticationNeededException(image);
            }
        }
        */
    }

    #region 

    public async Task<bool> SendMessageAsync(string message)
    {
        // Check if the conversation is open.
        Log("sending message ...");

        var isOpend = await IsChatOpened();
        if (isOpend)
        {
            Log("[x] Chat is opened");
            await OpenClientConversationAsync(_userPhone);
            // Send the message.

            var element = await _client.GetElementAsync(By.XPath(WhatsAppContants.MESSAGE_INPUT)) ?? throw new Exception();

            element.SendKeys(message);
            Log("[x] Message is written");
            element.SendKeys(Keys.Enter);
            Log("sending ...");

            //  [role="application"]>div:last-of-type
            var lastMessageSent = await _client.GetElementAsync(By.CssSelector("[role=\"application\"]>div:last-of-type"));
            if (lastMessageSent is null) return false;

            //  span[aria-label=" Delivered "]
            var deliveryStatus = await _client.WaitUntilElementAppearsAsync(By.CssSelector("span[aria-label=\" Delivered \"]"));
            return deliveryStatus;
        }
        else
        {
            return false;
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
        //_client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        //_client.Dispose();
        return ValueTask.CompletedTask;
    }

    public async Task<bool> IsAvaliableAsync()
    {
        var body = await _client.GetElementAsync(By.TagName("body"));
        return body is not null;
    }
}