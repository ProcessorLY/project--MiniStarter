using API.DTOs.WhatsAppTool;
using API.Modules.WhatsAppToolKit;
using API.Modules.WhatsAppToolKit.Exceptions;
using API.Modules.WhatsAppToolKit.Interfaces;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SQLitePCL;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers.WhatsAppControllers;

public class WhatsappController : BaseApiController//, IAsyncDisposable
{
    private bool _keepAlive = false;
    private readonly DataContext _context;
    private readonly IServiceProvider _service;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;

    private IWhatsAppMessangerAsync? _whatsAppMessanger;

    private string GetToken => this.HttpContext.Request.Headers["token"];

    public WhatsappController(DataContext context, IWebHostEnvironment webHostEnvironment, Microsoft.Extensions.Logging.ILogger<WhatsappController> logger, IServiceProvider service)
    {
        _logger = logger;
        _service = service;
        _context = context;
        _webHostEnvironment = webHostEnvironment;

    }

    private void Management()
    {
        var subscriperExists = WhatsAppSubscriper.WhatsAppSubscripers.ContainsKey(GetToken);
        if (!subscriperExists)
        {
            _whatsAppMessanger = _service.GetRequiredService<IWhatsAppMessangerAsync>();
            WhatsAppSubscriper.Add(GetToken, _whatsAppMessanger);
            _whatsAppMessanger.Open(GetToken);
        }
        else
        {
            _whatsAppMessanger = WhatsAppSubscriper.WhatsAppSubscripers![GetToken];
        }

        WhatsAppSubscriper.Refresh();
    }


    [HttpGet("Queue"), AllowAnonymous]
    public async Task<ActionResult> GetQueue()
    {
        var keys = WhatsAppSubscriper.WhatsAppSubscripers.Select(s => s.Key).ToArray();
        return Ok(new
        {
            Keys = keys,
        });
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult> GetHealth([FromBody] SendMessageDto messageDto)
    {
        try
        {
            Management();
            if (_whatsAppMessanger is null) throw new Exception("WhatsAppMessanger instance is null");

            var message = messageDto.Message;
            var receiver = messageDto.Phone;

            await _whatsAppMessanger.OpenClientConversationAsync(receiver);
            var status = await _whatsAppMessanger.SendMessageAsync(message);
            return Ok(new
            {
                Receiver = receiver,
                MessageSent = status,
                MessageContent = message,
            });
        }
        catch (QRCodeAuthenticationNeededException ex)
        {
            _keepAlive = true;
            return await GetImageResponse(ex);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [NonAction]
    private async Task<FileStreamResult> GetImageResponse(QRCodeAuthenticationNeededException qRCodeException)
    {
        var image = qRCodeException.QRImage;

        var filePath = GetPhysicalWhatsappQRFilePath();
        await image!.SaveAsPngAsync(filePath);

        var fileStream = GetPhysicalWhatsappQRFilePathStream(filePath);
        return File(fileStream, "image/jpeg");
    }

    [NonAction]
    private string GetPhysicalWhatsappQRFilePath()
    {
        var file = Guid.NewGuid() + ".jpeg";
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Documents", "Whatsapp", file);
        return path.TrimStart('/').Replace("\\", "/");
    }

    [NonAction]
    private Stream GetPhysicalWhatsappQRFilePathStream(string? path = null)
    {
        path ??= GetPhysicalWhatsappQRFilePath();
        return System.IO.File.OpenRead(path);
    }

    [NonAction]
    public async ValueTask DisposeAsync()
    {
        if (!_keepAlive)
        {
            WhatsAppSubscriper.Remove(GetToken);
            await _whatsAppMessanger.DisposeAsync();
        }
    }
}