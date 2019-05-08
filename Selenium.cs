using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Threading;

namespace AzureScheduledTrigger
{
    /// <summary>
    /// Summary description for MySeleniumTests
    /// </summary>
    [TestClass]
    public class MySeleniumTests
    {
        private TestContext testContextInstance;
        private IWebDriver driver;
        private string appURL;
        private string userName;
        private string passWord;

        public MySeleniumTests()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Run()
        {
            driver.Navigate().GoToUrl(appURL);

            this.AcceptTerms();
            this.GoToLogin();
            this.Login();
            //Assert.IsTrue(true, "Verified title of the page");
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void Setup()
        {
            appURL = "https://krunker.io/";
            userName = "testar12345";
            passWord = "testar12345";

            string browser = "Chrome";
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                //case "Firefox":
                //    driver = new FirefoxDriver();
                //    break;
                //case "IE":
                //    driver = new InternetExplorerDriver();
                //    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }

        [TestCleanup()]
        public void Cleanup()
        {
            driver.Quit();
        }

        private void AcceptTerms()
        {
            var consentWindow = driver.FindElement(By.Id("consentWindow"));
            var termsBtns = consentWindow.FindElements(By.ClassName("termsBtn"));
            var acceptBtn = termsBtns[termsBtns.Count - 1]; // Click the last btn
            acceptBtn.Click();
            Thread.Sleep(500);
        }

        private void GoToLogin()
        {
            var signedOutHeaderBar = driver.FindElement(By.Id("signedOutHeaderBar"));
            var loginBtns = signedOutHeaderBar.FindElements(By.ClassName("button"));
            var loginBtn = loginBtns[0]; //First button
            loginBtn.Click();
            Thread.Sleep(500);
        }

        private void Login()
        {
            var menuWindow = driver.FindElement(By.Id("menuWindow"));
            var accName = driver.FindElement(By.Id("accName"));
            var accPass = driver.FindElement(By.Id("accPass"));
            var accountButtons = menuWindow.FindElements(By.ClassName("accountButton"));
            var loginBtn = accountButtons[accountButtons.Count - 1]; // Last btn is Login
            
            accName.SendKeys(userName);
            accPass.SendKeys(passWord);

            loginBtn.Click();
            Thread.Sleep(500);
        }
    }
}