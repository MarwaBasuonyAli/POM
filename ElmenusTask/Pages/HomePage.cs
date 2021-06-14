using OpenQA.Selenium;

namespace ElmenusTask.Pages
{
    class HomePage: DriverOperations
    {
        #region objects
        private IWebElement _aSignUp => driver.FindElement(By.XPath("//a[text()='Sign up']"));
        private IWebElement _aLogin => driver.FindElement(By.XPath("//a[text()='Log in']"));
        #endregion objects

        /// <summary>
        /// Click on sign up button
        /// </summary>
        /// <returns> a new object from a signup page class</returns>
        public SignUpPage ClickSignUp()
        {
            _aSignUp.Click();
            return new SignUpPage();
        }

        /// <summary>
        /// click on sign in button 
        /// </summary>
        /// <returns>a new object from a sign in page class</returns>
        public LoginPage ClickLogin()
        {
            _aLogin.Click();
            return new LoginPage();
        }
    }
}
