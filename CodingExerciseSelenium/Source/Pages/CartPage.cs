using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExerciseSelenium.Source.Pages
{
    public class CartPage
    {
        private IWebDriver _webdriver;

        public CartPage(IWebDriver webdriver)
        {
            this._webdriver = webdriver;
        }

        #region Locators
        private readonly By _locatorCheckoutButton             = By.Id("checkout");
        private readonly By _locatorFirstNameInput             = By.Id("first-name");
        private readonly By _locatorLastNameInput              = By.Id("last-name");
        private readonly By _locatorPostalCodeInput            = By.Id("postal-code");
        private readonly By _locatorContinueButton             = By.Id("continue");
        private readonly By _locatorFinishCheckoutButton       = By.Id("finish");
        private readonly By _locatorFleeceJacketPriceTagInCart = By.XPath("//div[text()='Sauce Labs Fleece Jacket']/parent::a/following-sibling::div[@class='item_pricebar']/div[@class='inventory_item_price']");
        private readonly By _locatorOnesiePriceTagInCart       = By.XPath("//div[text()='Sauce Labs Onesie']/parent::a/following-sibling::div[@class='item_pricebar']/div[@class='inventory_item_price']");
        private readonly By _locatorOnesieRemoveButtonInCart   = By.XPath("//button[@data-test='remove-sauce-labs-onesie']");
        private readonly By _locatorFleeceJacketCountInCart    = By.XPath("//div[text()='Sauce Labs Fleece Jacket']/parent::a/parent::div/parent::div/div[@class='cart_quantity']");
        private readonly By _locatorItemTotalText              = By.XPath("//div[@class='summary_subtotal_label']");
        private readonly By _locatorThankYouText               = By.XPath("//h2[@class='complete-header']");

        #endregion

        #region Gets
        public string GetFleeceJacketPriceInCartText()
        {
            return _webdriver.FindElement(_locatorFleeceJacketPriceTagInCart).Text;
        }

        public string GetOnesiePriceInCartText()
        {
            return _webdriver.FindElement(_locatorOnesiePriceTagInCart).Text;
        }

        public string GetFreeceJacketQuantityInCartText()
        {
            return _webdriver.FindElement(_locatorFleeceJacketCountInCart).Text;
        }

        public string GetItemTotalAmountText()
        {
            return _webdriver.FindElement(_locatorItemTotalText).Text;
        }

        public string GetThankYouText()
        {
            return _webdriver.FindElement(_locatorThankYouText).Text;
        }

        #endregion

        #region SendKeys
        public void FillCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            _webdriver.FindElement(_locatorFirstNameInput).SendKeys(firstName);
            _webdriver.FindElement(_locatorLastNameInput).SendKeys(lastName);
            _webdriver.FindElement(_locatorPostalCodeInput).SendKeys(postalCode);
        }
        #endregion

        #region Clicks
        public void ClickOnTheCheckoutButton()
        {
            _webdriver.FindElement(_locatorCheckoutButton).Click();
        }

        public void ClickOnTheContinueButton()
        {
            _webdriver.FindElement(_locatorContinueButton).Click();
        }

        public void ClickOnTheFinishCheckoutButton()
        {
            _webdriver.FindElement(_locatorFinishCheckoutButton).Click();
        }

        public void ClickOnTheOnesieRemoveButtonInCart()
        {
            _webdriver.FindElement(_locatorOnesieRemoveButtonInCart).Click();
        }
        #endregion

        #region Booleans
        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public bool isThankYouTextDisplayedExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorThankYouText));
            if (element.Text != null)
            {
                return true;
            }
            else return false;
        }

        #endregion

    }
}
