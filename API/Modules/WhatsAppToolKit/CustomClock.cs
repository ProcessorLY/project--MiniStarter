using OpenQA.Selenium.Support.UI;


namespace API.Modules.WhatsAppToolKit;

public class CustomClock : IClock
{

    public DateTime Now => DateTime.Now;

    public bool IsNowBefore(DateTime otherDateTime)
    {
        return Now < otherDateTime;
    }

    public DateTime LaterBy(TimeSpan delay)
    {
        return Now.Add(delay);
    }
}
