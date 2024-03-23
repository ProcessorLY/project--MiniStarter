using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace API.Modules.WhatsAppToolKit.Extentions;

public static class ExpectedConditionsExtensions
{
    private static bool IsElementPresent(this IWebDriver webDriver, By by)
    {
        try
        {
            var elementToBeDisplayed = webDriver.FindElement(by);
            return elementToBeDisplayed is not null;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    public static bool WaituntilElementPresent(this IWebDriver webDriver, By by, int timeSpanTimeoutSeconds = 60, int timeSpanSeconds = 2)
    {
        try
        {
            var sleep = TimeSpan.FromSeconds(timeSpanSeconds);
            var timeout = TimeSpan.FromSeconds(timeSpanTimeoutSeconds);
            WebDriverWait driverWait = new WebDriverWait(new CustomClock(), webDriver, timeout, sleep);
            var status = driverWait.Until(c => c.IsElementPresent(by));

            var elementToBeDisplayed = webDriver.FindElement(by);
            return elementToBeDisplayed is not null;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
    public static bool IsElementDisplayed(this IWebDriver webDriver, By by)
    {
        try
        {
            var elementToBeDisplayed = webDriver.FindElement(by);
            return elementToBeDisplayed.Displayed;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
    public static bool IsPageFinishLoading(this IWebDriver webDriver)
    {
        try
        {
            return ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete");
        }
        catch (Exception)
        {
            return false;
        }
    }
}
