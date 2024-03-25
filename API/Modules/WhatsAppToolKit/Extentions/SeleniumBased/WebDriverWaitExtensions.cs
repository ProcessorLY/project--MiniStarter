using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace API.Modules.WhatsAppToolKit.Extentions.SeleniumBased;

public static class WebDriverWaitExtensions
{

    public static IWebElement? WaitUntilElementPresent(this WebDriver driver, By by, int timeSpanTimeoutSeconds = 60, int timeSpanSeconds = 2)
    {
        var status = driver.WaituntilElementPresent(by);
        return status ? driver.FindElement(by) : null;
    }

    public static bool WaitUntilPageComplate(this WebDriver driver, int timeSpanTimeoutSeconds = 60, int timeSpanSeconds = 2)
    {
        var sleep = TimeSpan.FromSeconds(timeSpanSeconds);
        var timeout = TimeSpan.FromSeconds(timeSpanTimeoutSeconds);
        WebDriverWait driverWait = new WebDriverWait(new CustomClock(), driver, timeout, sleep);
        return driverWait.Until(c => c.IsPageFinishLoading());
    }

    /*
    public static IWebElement WaitUntilElementIsVisible(this WebDriver driver, By by, int timeSpanTimeoutSeconds = 10, int timeSpanSeconds = 10)
    {
        var sleep = TimeSpan.FromSeconds(timeSpanSeconds);
        var timeout = TimeSpan.FromSeconds(timeSpanTimeoutSeconds);

        WebDriverWait driverWait = new WebDriverWait(new CustomClock(), driver, timeout, sleep);
        return driverWait.Until(ExpectedConditions.ElementIsVisible(by));
    }

    / *
    public static IWebElement WaitUntilElementIsClickable(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeClickable(by));
    }

    public static IWebElement WaitUntilElementIsSelected(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementIsPresent(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.PresenceOfElementLocated(by));
    }

    public static IWebElement WaitUntilElementIsStale(this WebDriverWait wait, IWebElement element)
    {
        return wait.Until(ExpectedConditions.StalenessOf(element));
    }

    public static IWebElement WaitUntilElementIsInvisible(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
    }

    public static IWebElement WaitUntilElementIsRefreshed(this WebDriverWait wait, IWebElement element)
    {
        return wait.Until(ExpectedConditions.Refreshed(element));
    }

    public static IWebElement WaitUntilElementIsEnabled(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeEnabled(by));
    }

    public static IWebElement WaitUntilElementIsDisabled(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeDisabled(by));
    }

    public static IWebElement WaitUntilElementHasAttribute(this WebDriverWait wait, By by, string attributeName)
    {
        return wait.Until(ExpectedConditions.ElementAttributesToContain(by, attributeName));
    }

    public static IWebElement WaitUntilElementHasText(this WebDriverWait wait, By by, string text)
    {
        return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
    }

    public static IWebElement WaitUntilElementHasCssClass(this WebDriverWait wait, By by, string className)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementHasStyle(this WebDriverWait wait, By by, string styleName)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementHasSize(this WebDriverWait wait, By by, Size size)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementHasLocation(this WebDriverWait wait, By by, Point location)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementIsChecked(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementIsUnchecked(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementIsSelected(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }

    public static IWebElement WaitUntilElementIsDeselected(this WebDriverWait wait, By by)
    {
        return wait.Until(ExpectedConditions.ElementToBeSelected(by));
    }


    */
}