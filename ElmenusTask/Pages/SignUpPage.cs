using OpenQA.Selenium;

namespace ElmenusTask.Pages
{
    class SignUpPage : DriverOperations
    {
        #region objects
        private IWebElement _inpName => driver.FindElement(By.CssSelector("section[label='Full Name'] input"));
        private IWebElement _inpEmail => driver.FindElement(By.XPath("(//section[@label='Email']//input)[2]"));
        private IWebElement _labAgreement = driver.FindElement(By.XPath("//span[text()=\"I agree to Zomato's \"]/ancestor::div/label"));
        private IWebElement _butACreateAccount => driver.FindElement(By.XPath("//span[text()='Create account']/ancestor::button"));
        private IWebElement _labOTP => driver.FindElement(By.CssSelector("section[label='OTP'] input"));
        private IWebElement _butProceed => driver.FindElement(By.XPath("//span[text()='Proceed']/ancestor::button"));

        #endregion objects

        /// <summary>
        /// Signup with inserting userName and email
        /// </summary>
        /// <param name="name">user name as a string</param>
        /// <param name="email"> user email as a string</param>
        public void SignUp(string name, string email)
        {
            _inpName.SendKeys(name);
            _inpEmail.SendKeys(email);
            if (!_labAgreement.Selected)
            {
                _labAgreement.Click();
            }
            _butACreateAccount.Click();
        }

        /// <summary>
        /// insert otp and click on proceed button
        /// </summary>
        /// <param name="otp">otp as a string</param>
        public void VerifyOTP(string otp)
        {
            _labOTP.SendKeys(otp);
            _butProceed.Click();
        }

        /// <summary>
        /// get the error after inserting wrong OTP
        /// </summary>
        /// <returns>the error message as a string</returns>
        public string GetOtpError()
        {
            IWebElement otpError = driver.FindElement(By.XPath("//span[text()='Proceed']/ancestor::section//p[2]"));
            return otpError.Text;
        }
    }
}
