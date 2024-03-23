namespace API.Modules.WhatsAppToolKit.Exceptions;

public class WhatsAppException : Exception
{
    public WhatsAppException() : base("A WhatsApp error occurred.")
    {
    }

    public WhatsAppException(string message) : base(message)
    {
    }
}
