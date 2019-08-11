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
            GoToContactsDetailsPage();
            InitContactModification();
            FillModificationContactForm(contact);
            SubmitContactModification();
            ReturnHomePage();
            return this;
        }

        public ContactHelper IfExistAnyContact()
        {
            manager.Navigator.GoToHomePage();
            if (!IsExistRecords())
            {
                ContactData contact = new ContactData();
                contact.Lastname = "";

                Create(contact);
            }
            return this;
        }

        public ContactHelper Remove()
        {
            manager.Navigator.GoToHomePage();
            SelectContact();
            RemoveContact();
            CloseAlert();
            return this;
        }
        
        public ContactHelper GoToContactsDetailsPage()
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'][1])")).Click();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr"));
            foreach(IWebElement element in elements)
            {
                contacts.Add(new ContactData(element.Text));
            }
            return contacts;
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

            //driver.FindElement(By.Name("new_group")).Click();
            //new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.New_group);
            //driver.FindElement(By.XPath("//select[5]/option[" + contact.New_group_id + "]")).Click();

            return this;
        }

        public ContactHelper FillModificationContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper ReturnHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
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
