using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CodingExerciseSelenium.Source.Pages
{
    public class LoginPage
    {
        private IWebDriver _webdriver;

        public LoginPage(IWebDriver webdriver)
        {
            this._webdriver = webdriver;
        }

        #region Locators
        //Consider FindsBy method (PageFactory) to get locators instead of this one, or even XPath. Investigate difference
        private readonly By _locatorUsernameInput     = By.Id("user-name");
        private readonly By _locatorPasswordInput     = By.Id("password");
        private readonly By _locatorLoginButton       = By.Id("login-button");
        private readonly By _locatorLockedOutText     = By.XPath("//h3[@data-test='error']");  //prioritize XPath due to more flexibility for complex selectors

        #endregion

        #region Gets
        public string GetLockedOutText()
        {
            return _webdriver.FindElement(_locatorLockedOutText).Text;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public string GetLockedOutTextExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorLockedOutText));
            return element.Text;
        }

        #endregion

        #region SendKeys
        public void SendKeysToUsernameTextField(string username)
        {
            _webdriver.FindElement(_locatorUsernameInput).SendKeys(username);
        }

        public void SendKeysToPasswordTextField(string password)
        {
            _webdriver.FindElement(_locatorPasswordInput).SendKeys(password);
        }
        #endregion

        #region Clicks
        public void ClickOnTheLoginButton()
        {
            _webdriver.FindElement(_locatorLoginButton).Click();
        }
        #endregion
    }
}
