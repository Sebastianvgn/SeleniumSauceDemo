using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExerciseSelenium.Source.Pages
{
    public class InventoryPages
    {
        private IWebDriver _webdriver;

        public InventoryPages(IWebDriver webdriver)
        {
            this._webdriver = webdriver;
        }

        #region Locators
        private readonly By _locatorProductsText = By.ClassName("title");
        private readonly By _locatorBikeLightAddButton = By.XPath("//button[@data-test='add-to-cart-sauce-labs-bike-light']");
        private readonly By _locatorBikeLightRemoveButton = By.XPath("//button[@data-test='remove-sauce-labs-bike-light']");
        private readonly By _locatorShoppingCartCounter = By.XPath("//span[@class='shopping_cart_badge']");
        private readonly By _locatorShoppingCartButton = By.XPath("//a[@class='shopping_cart_link']");
        private readonly By _locatorMenuBurger = By.Id("react-burger-menu-btn");
        private readonly By _locatorLogoutButton = By.Id("logout_sidebar_link");
        private readonly By _locatorProductSort = By.XPath("//select[@class='product_sort_container']");
        private readonly By _locatorProductSortText = By.XPath("//span[@class='active_option']");
        private readonly By _locatorFleeceJacketAddButton = By.XPath("//button[@data-test='add-to-cart-sauce-labs-fleece-jacket']");
        private readonly By _locatorFleeceJacketRemoveButton = By.XPath("//button[@data-test='remove-sauce-labs-fleece-jacket']");
        private readonly By _locatorOnesieAddButton = By.XPath("//button[@data-test='add-to-cart-sauce-labs-onesie']");
        private readonly By _locatorOnesieRemoveButton = By.XPath("//button[@data-test='remove-sauce-labs-onesie']");
        private readonly By _locatorFleeceJacketPriceTag = By.XPath("//div[text()='Sauce Labs Fleece Jacket']/parent::a/parent::div/following-sibling::div/div[@class='inventory_item_price']");
        private readonly By _locatorOnesiePriceTag = By.XPath("//div[text()='Sauce Labs Onesie']/parent::a/parent::div/following-sibling::div/div[@class='inventory_item_price']");
        private readonly By _locatorPricesOfProductsText = By.XPath("//div[@class='inventory_item_price']");
        

        #endregion

        #region Gets
        public string GetProductsText()
        {
            return _webdriver.FindElement(_locatorProductsText).Text;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public string GetProductsTextExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorProductsText));
            return element.Text;
        }

        public string GetSortText()
        {
            return _webdriver.FindElement(_locatorProductSortText).Text;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public string GetSortTextExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorProductSortText));
            return element.Text;
        }

        public string GetFleeceJacketPriceText()
        {
            return _webdriver.FindElement(_locatorFleeceJacketPriceTag).Text;
        }

        public string GetOnesiePriceText()
        {
            return _webdriver.FindElement(_locatorOnesiePriceTag).Text;
        }

        public string GetCartCounterText()
        {
            return _webdriver.FindElement(_locatorShoppingCartCounter).Text;
        }

        //List is being used since the locator exists for more than 1 element. LINQ used for better readability and support.
        public List<string> GetListOfPricesText()
        {
            List<string> elementTexts = _webdriver.FindElements(_locatorPricesOfProductsText).Select(iw => iw.Text).ToList();
            return elementTexts;
        }

        #endregion

        #region SendKeys

        #endregion

        #region Clicks
        public void ClickOnTheBikeLightAddToCartButton()
        {
            _webdriver.FindElement(_locatorBikeLightAddButton).Click();
        }

        public void ClickOnTheShoppingCartButton()
        {
            _webdriver.FindElement(_locatorShoppingCartButton).Click();
        }

        public void ClickOnTheMenuBurgerButton()
        {
            _webdriver.FindElement(_locatorMenuBurger).Click();
        }

        public void ClickOnTheLogoutButton()
        {
            _webdriver.FindElement(_locatorLogoutButton).Click();
        }

        public void ClickOnTheProductSort()
        {
            _webdriver.FindElement(_locatorProductSort).Click();
        }

        public void ClickOnTheDesiredSortOption(string option)
        {
            var path = By.XPath($"//option[text() ='{option}']");  //This XPath will change depending on which sort is desired. Only the text of the sort is needed
            _webdriver.FindElement(path).Click();
        }

        public void ClickOnTheFleeceJacketAddToCartButton()
        {
            _webdriver.FindElement(_locatorFleeceJacketAddButton).Click();
        }

        public void ClickOnTheOnesieAddToCartButton()
        {
            _webdriver.FindElement(_locatorOnesieAddButton).Click();
        }

        #endregion

        #region Booleans

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public bool isBikeLightRemoveButtonDisplayedExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorBikeLightRemoveButton));
            if (element.Text != null)
            {
                return true;
            }
            else return false;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public bool isCartCounterDisplayedExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorShoppingCartCounter));
            if (element.Text != null)
            {
                return true;
            }
            else return false;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public bool isFleeceJacketRemoveButtonDisplayedExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorFleeceJacketRemoveButton));
            if (element.Text != null)
            {
                return true;
            }
            else return false;
        }

        //Explicit Wait is used to wait for a certain amount of time for the element to load 
        public bool isOnesieRemoveButtonDisplayedExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_locatorOnesieRemoveButton));
            if (element.Text != null)
            {
                return true;
            }
            else return false;
        }

        #endregion
    }
}
