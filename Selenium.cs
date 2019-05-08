using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Configuration;

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
        private object script;
        IJavaScriptExecutor js;
        private string appURL;

        public MySeleniumTests()
        {
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void Run()
        {
            driver.Navigate().GoToUrl(appURL);
            this.SetLocalStorage();
            this.ClaimFreeKR();

            //this.AcceptTerms();
            //this.GoToLogin();
            //this.Login();

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
            driver = new ChromeDriver();
            js = driver as IJavaScriptExecutor;
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
            
            accName.SendKeys("");
            accPass.SendKeys("");

            loginBtn.Click();
            Thread.Sleep(500);
        }

        private void SetLocalStorage()
        {
            var consent = ConfigurationManager.AppSettings["consent"];
            var krunker_token = ConfigurationManager.AppSettings["krunker_token"];
            var krunker_username = ConfigurationManager.AppSettings["krunker_username"];
            var krunker_id = ConfigurationManager.AppSettings["krunker_id"];

            script = js.ExecuteScript("return localStorage.setItem('consent', '" + consent + "')");
            script = js.ExecuteScript("return localStorage.setItem('krunker_token', '" + krunker_token + "')");
            script = js.ExecuteScript("return localStorage.setItem('krunker_username', '" + krunker_username + "')");
            script = js.ExecuteScript("return localStorage.setItem('krunker_id', '" + krunker_id + "')");
            script = js.ExecuteScript("return window.location.href = window.location.href");

            Thread.Sleep(1500);
        }

        private void ClaimFreeKR()
        {
            var claimImg = driver.FindElement(By.Id("claimImg"));
            claimImg.Click();
        }
    }
}