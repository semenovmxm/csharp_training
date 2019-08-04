using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase

    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            InitNewContactCreation();
            FillNewContactForm(contact);
            SubmitContactCreation();
            ReturnHomePage();
            return this;
        }
        public ContactHelper Modify(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            if (!IsExistRecords())
            {
                Create(contact);
            }
            GoToContactsDetailsPage();
            InitContactModification();
            FillModificationContactForm(contact);
            SubmitContactModification();
            ReturnHomePage();
            return this;
        }
        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            if (!IsExistRecords())
            {
                Create(contact);
            }
            SelectContact(contact);
            RemoveContact();
            CloseAlert();
            return this;
        }
        
        public ContactHelper GoToContactsDetailsPage()
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.Name("modifiy")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])")).Click();
            return this;
        }
            public ContactHelper FillNewContactForm(ContactData contact)
        {
            FillModificationContactForm(contact);

            driver.FindElement(By.Name("new_group")).Click();
            //new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.New_group);
            driver.FindElement(By.XPath("//select[5]/option[" + contact.New_group_id + "]")).Click();

            return this;
        }

        public ContactHelper FillModificationContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Nickname);
            //Type(By.Name("nickname"), contact.Firstname);
            //Type(By.Name("title"), contact.Title);
            //Type(By.Name("company"), contact.Company);
            //Type(By.Name("address"), contact.Address);
            //Type(By.Name("home"), contact.Home);
            //Type(By.Name("mobile"), contact.Mobile);
            //Type(By.Name("work"), contact.Work);
            //Type(By.Name("fax"), contact.Fax);
            //Type(By.Name("email"), contact.Email);

            //driver.FindElement(By.Name("email")).Click();
            //driver.FindElement(By.Name("email")).Clear();
            //driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            //driver.FindElement(By.Name("email2")).Click();
            //driver.FindElement(By.Name("email2")).Clear();
            //driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            //driver.FindElement(By.Name("email3")).Click();
            //driver.FindElement(By.Name("email3")).Clear();
            //driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);
            //driver.FindElement(By.Name("homepage")).Click();
            //driver.FindElement(By.Name("homepage")).Clear();
            //driver.FindElement(By.Name("homepage")).SendKeys(contact.Homepage);
            //driver.FindElement(By.Name("bday")).Click();
            //new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            //driver.FindElement(By.XPath("//option[@value='" + contact.Bday + "']")).Click();
            //driver.FindElement(By.Name("bmonth")).Click();
            //new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            //driver.FindElement(By.XPath("//option[@value='" + contact.Bmonth + "']")).Click();
            //driver.FindElement(By.Name("byear")).Click();
            //driver.FindElement(By.Name("byear")).Clear();
            //driver.FindElement(By.Name("byear")).SendKeys(contact.Byear);
            //driver.FindElement(By.Name("aday")).Click();
            //new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            //driver.FindElement(By.XPath("(//option[@value='" + contact.Aday + "'])")).Click();
            //new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            //driver.FindElement(By.XPath("(//option[@value='" + contact.Amonth + "'])")).Click();
            //driver.FindElement(By.Name("ayear")).Click();
            //driver.FindElement(By.Name("ayear")).Clear();
            //driver.FindElement(By.Name("ayear")).SendKeys(contact.Ayear);
            //driver.FindElement(By.Name("address2")).Click();
            //driver.FindElement(By.Name("address2")).Clear();
            //driver.FindElement(By.Name("address2")).SendKeys(contact.Address2);
            //driver.FindElement(By.Name("phone2")).Click();
            //driver.FindElement(By.Name("phone2")).Clear();
            //driver.FindElement(By.Name("phone2")).SendKeys(contact.Phone2);
            //driver.FindElement(By.Name("notes")).Click();
            //driver.FindElement(By.Name("notes")).Clear();
            //driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);
            return this;
        }

        public ContactHelper ReturnHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper SelectContact(ContactData contact)
        {
            driver.FindElement(By.Name("selected[]")).Click();
            //driver.FindElement(By.Id(contact.Index)).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            return this;
        }
        public ContactHelper CloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

    }
}
