using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using CodingExerciseSelenium.Source.Pages;

namespace CodingExerciseSelenium.Tests
{
    public class Tests
    {
        private IWebDriver _webdriver;

        [SetUp]
        public void Setup()
        {
            //Initialize chromeDriver
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webdriver = new ChromeDriver();
            _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _webdriver.Manage().Window.Maximize();
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Sebastian Villa Garcia")]
        public void SuccessfulLoginTest()
        {
            //Variables
            string username = "standard_user";
            string password = "secret_sauce";

            // Initialize pages
            LoginPage loginPage = new LoginPage(_webdriver);
            InventoryPages inventoryPage = new InventoryPages(_webdriver);

            // URL
            _webdriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            loginPage.SendKeysToUsernameTextField(username);
            loginPage.SendKeysToPasswordTextField(password);
            loginPage.ClickOnTheLoginButton();

            // Verify product page            
            Assert.That(inventoryPage.GetProductsTextExplicitWait().Equals("Products"), $"The products text is: {inventoryPage.GetProductsText()}");
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Sebastian Villa Garcia")]
        public void FailedLoginTest()
        {
            //Variables
            string username = "locked_out_user";
            string password = "secret_sauce";
            string errorMessage = "Epic sadface: Sorry, this user has been locked out.";

            // Initialize pages
            LoginPage loginPage = new LoginPage(_webdriver);

            // URL
            _webdriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            loginPage.SendKeysToUsernameTextField(username);
            loginPage.SendKeysToPasswordTextField(password);
            loginPage.ClickOnTheLoginButton();

            // Verify Login failed        
            Assert.That(loginPage.GetLockedOutTextExplicitWait().Equals(errorMessage), $"The products text is: {loginPage.GetLockedOutText()}");
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Sebastian Villa Garcia")]
        public void HappyPathWorkflowTest()
        {
            //Variables
            string username = "standard_user";
            string password = "secret_sauce";
            string firstName = "Sebastian";
            string lastName = "Villa Garcia";
            string postalCode = "12345";

            // Initialize pages
            LoginPage loginPage = new LoginPage(_webdriver);
            InventoryPages inventoryPage = new InventoryPages(_webdriver);
            CartPage cartPage = new CartPage(_webdriver);

            // URL
            _webdriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            loginPage.SendKeysToUsernameTextField(username);
            loginPage.SendKeysToPasswordTextField(password);
            loginPage.ClickOnTheLoginButton();

            //Add to cart
            inventoryPage.ClickOnTheBikeLightAddToCartButton();
            inventoryPage.ClickOnTheShoppingCartButton();

            //Checkout workflow
            cartPage.ClickOnTheCheckoutButton();
            cartPage.FillCheckoutInformation(firstName, lastName, postalCode);
            cartPage.ClickOnTheContinueButton();
            cartPage.ClickOnTheFinishCheckoutButton();

            //Logout
            inventoryPage.ClickOnTheMenuBurgerButton();
            inventoryPage.ClickOnTheLogoutButton();
        }

        [Test, Category("Regression"), Category("Production"), Category("Stage"), Category("Test")]
        [Author("Sebastian Villa Garcia")]
        public void MultipleScenariosWorkflowTest()
        {
            //Variables
            string username = "standard_user";
            string password = "secret_sauce";
            string firstName = "Sebastian";
            string lastName = "Villa Garcia";
            string postalCode = "12345";
            string sort = "Price (low to high)";
            string thankYouText = "Thank you for your order!";
            decimal value1 = 0;
            decimal value2 = 0;

            // Initialize pages
            LoginPage loginPage = new LoginPage(_webdriver);
            InventoryPages inventoryPage = new InventoryPages(_webdriver);
            CartPage cartPage = new CartPage(_webdriver);

            // URL
            _webdriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // Login
            loginPage.SendKeysToUsernameTextField(username);
            loginPage.SendKeysToPasswordTextField(password);
            loginPage.ClickOnTheLoginButton();

            //Change sort and assert if updated
            inventoryPage.ClickOnTheProductSort();
            inventoryPage.ClickOnTheDesiredSortOption(sort);
            Assert.That(inventoryPage.GetSortTextExplicitWait().Equals(sort), $"The sort text is: {inventoryPage.GetSortText()}");

            //Verify prices are in correct order
            List<string> listOfPrices = inventoryPage.GetListOfPricesText();
            for (int i = 0; i < listOfPrices.Count - 1; i++)
            {
                value1 = decimal.Parse(listOfPrices[i], System.Globalization.NumberStyles.Currency);
                value2 = decimal.Parse(listOfPrices[i+1], System.Globalization.NumberStyles.Currency);
                Assert.That(value1 <= value2 , $"Products {i+1} and {i+2} are not ordered correctly");
            }

            //Add to cart
            inventoryPage.ClickOnTheFleeceJacketAddToCartButton();
            inventoryPage.ClickOnTheOnesieAddToCartButton();
            Assert.That(inventoryPage.isFleeceJacketRemoveButtonDisplayedExplicitWait(), "The Remove button for Sauce Labs Fleece Jacket is not displayed.");
            Assert.That(inventoryPage.isOnesieRemoveButtonDisplayedExplicitWait(), "The Remove button for Sauce Labs Onesie is not displayed.");

            //Save price tags
            string fleeceJacketPrice = inventoryPage.GetFleeceJacketPriceText();
            string onesiePrice = inventoryPage.GetOnesiePriceText();

            //Assert cart count
            Assert.That(inventoryPage.GetCartCounterText().Equals("2"), $"The cart counter should be 2. Instead it is displaying {inventoryPage.GetCartCounterText()}");
            
            //Verify prices in cart
            inventoryPage.ClickOnTheShoppingCartButton();
            Assert.That(cartPage.GetOnesiePriceInCartText().Equals(onesiePrice), $"Sauce Labs Onesie price in cart is different from the Product list. It displays {cartPage.GetOnesiePriceInCartText()}");
            Assert.That(cartPage.GetFleeceJacketPriceInCartText().Equals(fleeceJacketPrice), $"Sauce Labs Fleece Jacket price in cart is different from the Product list. It displays {cartPage.GetFleeceJacketPriceInCartText()}");
            cartPage.ClickOnTheOnesieRemoveButtonInCart();

            //Cart count assert
            string freeceJacketCount = cartPage.GetFreeceJacketQuantityInCartText();
            string cartCount = inventoryPage.GetCartCounterText();
            Assert.That(freeceJacketCount.Equals(cartCount), "Cart count is not displaying the correct amount of items");

            //Checkout workflow
            cartPage.ClickOnTheCheckoutButton();
            cartPage.FillCheckoutInformation(firstName, lastName, postalCode);
            cartPage.ClickOnTheContinueButton();
            string itemTotalAmount = cartPage.GetItemTotalAmountText();
            Assert.That(itemTotalAmount.Contains(fleeceJacketPrice), $"Sauce Labs Fleece Jacket Item total amount is not displayed correctly. Instead, it's displaying {itemTotalAmount}");
            cartPage.ClickOnTheFinishCheckoutButton();

            //Verify Thank you text is displayed, and it displays the correct message
            Assert.That(cartPage.isThankYouTextDisplayedExplicitWait(), "Thank you text is not being displayed");
            Assert.That(cartPage.GetThankYouText().Equals(thankYouText), $"Thank you text is not displaying the correct sentence. Instead, it's displaying {cartPage.GetThankYouText()}");

            //Logout
            inventoryPage.ClickOnTheMenuBurgerButton();
            inventoryPage.ClickOnTheLogoutButton();
        }

        [TearDown]
        public void CloseBrowser()
        {
            // Close the driver instance.
            _webdriver.Close();
            _webdriver.Quit();
        }
    }
}
