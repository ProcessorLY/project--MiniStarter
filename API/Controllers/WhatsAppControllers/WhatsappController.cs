using API.Modules.WhatsAppToolKit.Exceptions;
using API.Modules.WhatsAppToolKit.Interfaces;
using Microsoft.Extensions.FileProviders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace API.Controllers.WhatsAppControllers;

public class WhatsappController : BaseApiController
{
    private readonly DataContext _context;
    private readonly IWhatsAppMessangerAsync _whatsAppMessanger;

    public WhatsappController(DataContext context, IWhatsAppMessangerAsync whatsAppMessanger)
    {
        _context = context;
        _whatsAppMessanger = whatsAppMessanger;
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult> GetHealth()
    {
        try
        {
            await _whatsAppMessanger.OpenClientConversationAsync("0915951905");
            await _whatsAppMessanger.SendMessageAsync("HI");
            return Ok();
        }
        catch (QRCodeAuthenticationNeededException ex)
        {
            var image = ex.QRImage;
            var file = "./Documents/Whatsapp/" + Guid.NewGuid() + ".png";
            await image!.SaveAsPngAsync(file);
            return File(file, "image/jpeg");

            //using (var str = new MemoryStream())
            //{
            //    await image!.SaveAsPngAsync(str);
            //    str.Position = 0;
            //    return File(str, "image/jpeg");
            //}

        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
