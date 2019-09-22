using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            string url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        public void DeleteAccount(AccountData account)
        {
            driver.Url = "http://localhost/mantisbt-2.22.0/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("form[id='manage-user-delete-form']")).Click();
            driver.FindElement(By.CssSelector("input.btn")).Click();
        }

        private string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void FillPasswordForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("realname")).SendKeys(account.Name);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        private void SubmitPasswordForm()
        {
            driver.FindElement(By.XPath("//button/span")).Click();
        }

        private void OpenRegistrationForm()
        {
            driver.FindElement(By.LinkText("Зарегистрировать новую учётную запись")).Click();
            //driver.FindElements(By.CssSelector("span.bracket-link"))[0].Click();
        }

        private void SubmitRegistration()
        {
            driver.FindElement(By.XPath("//input[@value='Зарегистрироваться']")).Click();
            //driver.FindElement(By.CssSelector("input.button")).Click();
        }

        private void FillRegistrationForm(AccountData account)
        {            
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        private void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.22.0"; 
        }
    }
}
