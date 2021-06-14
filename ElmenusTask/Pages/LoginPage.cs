using OpenQA.Selenium;

namespace ElmenusTask.Pages
{
    class LoginPage : DriverOperations
    {
        #region objects
        private IWebElement _spaContinueWithEmail => driver.FindElement(By.XPath("//span[text()='Continue with Email']"));
        private IWebElement _inpEmail => driver.FindElement(By.XPath("(//section[@label='Email']//input)[2]"));
        private IWebElement _butSendOTP => driver.FindElement(By.XPath("//span[text()='Send OTP']/ancestor::button"));

        #endregion objects

        /// <summary>
        /// click on continue with email button 
        /// </summary>
        public void ClickOnContinueWithEmail()
        {
            _spaContinueWithEmail.Click();
        }

        /// <summary>
        /// Add email and click on send OTP button
        /// </summary>
        /// <param name="email">email as a string</param>
        public void AddEmailAndSendOTP(string email)
        {
            _inpEmail.SendKeys(email);
            _butSendOTP.Click();
        }

        /// <summary>
        /// get the erorr if user inserts wrong email
        /// </summary>
        /// <returns>error as a string</returns>
        public string GetError()
        {
            IWebElement otpError = driver.FindElement(By.XPath("(//span[text()='Send OTP']/ancestor::section//div)[3]"));
            return otpError.Text;
        }
    }
}
