using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using ElmenusTask.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Threading;
using Assert = NUnit.Framework.Assert;
using TestContext = NUnit.Framework.TestContext;

namespace ElmenusTask
{
    [TestFixture]
    class SeleniumTestCase: DriverOperations
    {
        protected ExtentReports _extent;
        protected ExtentTest _test;
        string projDir;

        [OneTimeSetUp]
        public void BeforeClass()
        {
            try
            {
                _extent = new ExtentReports();
                projDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                DirectoryInfo di = Directory.CreateDirectory(projDir + "\\Test_Execution_Reports");
                var htmlReporter = new ExtentHtmlReporter(di + "\\Automation_Report" + ".html");
                _extent.AddSystemInfo("Environment", "Technical Test");
                _extent.AddSystemInfo("User Name", "Marwa");
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [SetUp]
        public void BeforeTest()
        {
            try
            {
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
                driver.Navigate().GoToUrl("https://www.zomato.com/");
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [Test]
        public void CheckForErrorIfWrongOTPInserted()
        {
            HomePage home = new HomePage();
            SignUpPage signUp =  home.ClickSignUp();
            System.Random random = new System.Random();
            string email = "marwa" + random.Next() + "@email.com";
            signUp.SignUp("Marwa", email);
            signUp.VerifyOTP("1234");
            Assert.AreEqual("Invalid verification code", signUp.GetOtpError());
        }

        [Test]
        public void CheckForLoginWithNotRegisteredEmail()
        {
            HomePage home = new HomePage();
            LoginPage signIn = home.ClickLogin();
            System.Random random = new System.Random();
            string email = "marwa1" + random.Next() + "@email.com";
            signIn.ClickOnContinueWithEmail();
            signIn.AddEmailAndSendOTP(email);
            Assert.AreEqual("This email is not registered with us. Please sign up.", signIn.GetError());

        }

        [TearDown]
        public void AfterTest()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        _test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        _test.Log(logstatus, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
            driver.Quit();
        }

        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

                projDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                DirectoryInfo di = Directory.CreateDirectory(projDir + @"\Defect_Screenshots");
                string finalpth = di +@"\"+ screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }
    }
}
