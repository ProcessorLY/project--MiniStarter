namespace API.Modules.WhatsAppToolKit.Exceptions;

public class PhoneNumberDoesNotHaveWhatsAppException : Exception
{
    public PhoneNumberDoesNotHaveWhatsAppException() : base("The specified phone number does not have WhatsApp installed.")
    {
    }
}
