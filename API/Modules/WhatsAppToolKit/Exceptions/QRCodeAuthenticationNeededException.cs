using SixLabors.ImageSharp;

namespace API.Modules.WhatsAppToolKit.Exceptions;

public class QRCodeAuthenticationNeededException : Exception
{
    public Image? QRImage { get; private set; }
    public QRCodeAuthenticationNeededException(Image image) : base("QR code authentication is needed to log in to WhatsApp.")
    {
        QRImage = image;
    }
}
