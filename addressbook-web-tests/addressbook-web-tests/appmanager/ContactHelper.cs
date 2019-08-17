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

        public List<ContactData> contactCashe = null;
        public List<ContactData> GetContactList()
        {
            if(contactCashe == null)
            {
                contactCashe = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=\"entry\"]"));
                foreach (IWebElement element in elements)
                {
                    ContactData contact = new ContactData(element.Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };
                    contactCashe.Add(contact);
                }
                //contactCashe.RemoveAt(0);
            }
            return new List<ContactData>(contactCashe);
        }

        public int GetContactCount()
        {
           return driver.FindElements(By.CssSelector("tr[name=\"entry\"]")).Count;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.Name("modifiy")).Click();
            return this;
        }

        public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCashe = null;
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
            contactCashe = null;
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
            contactCashe = null;
            return this;
        }
        public ContactHelper CloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].
                FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");          
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");            
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");


            //string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            //string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            //string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            //string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            //string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            //string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            //string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            //string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            //string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }


    }
}
