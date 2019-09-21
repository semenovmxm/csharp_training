using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase

    {
        private string baseURL;

        public ManagementMenuHelper(ApplicationManager manager, string baseURL) : base(manager) 
        {
            this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseURL) 
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }

        public void GoToManagementProjectPage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php")
            {
                return;
            }
            GoToManageOverviewPage();
            driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.22.0/manage_proj_page.php'])")).Click();
        }

        private void GoToManageOverviewPage()
        {
            if (driver.Url == baseURL + "/manage_overview_page.php")
            {
                return;
            }
            driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.22.0/manage_overview_page.php'])")).Click();
        }
    }
}
