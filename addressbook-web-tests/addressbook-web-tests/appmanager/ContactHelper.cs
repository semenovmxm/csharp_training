﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public ContactHelper IfExistAnyContactInAnyGroup()
        {
            
            manager.Groups.IfExistAnyGroup();
            IfExistAnyContact();
            return this;
        }

        public ContactHelper Modify(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            GoToContactsDetailsPage(contact);
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

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contact.Id);
            RemoveContact();
            CloseAlert();
            return this;
        }
        
        public ContactHelper GoToContactsDetailsPage()
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'][1])")).Click();
            return this;
        }

        public ContactHelper GoToContactsDetailsPage(ContactData contact)
        {
            driver.FindElement(By.XPath("(//a[@href='view.php?id=" + contact.Id + "'])")).Click();
            return this;
        }
        public List<ContactData> contactCashe = null;
        public List<ContactData> GetContactList_Old()
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

        public List<ContactData> GetContactList()
        {
            if (contactCashe == null)
            {
                contactCashe = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    string lastName = cells[1].Text;
                    string firstName = cells[2].Text;
                    string address = cells[3].Text;
                    string allEmails = cells[4].Text;
                    string allPhones = cells[5].Text;
                    ContactData contact = new ContactData(firstName, lastName)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };
                    contactCashe.Add(contact);
                }
                //contactCashe.RemoveAt(0);
            }
            return new List<ContactData>(contactCashe);
        }

        public ContactData FindContactForGroup(List<ContactData> contactList)
        {
            ContactData contact = ContactData.GetAll().Except(contactList).First();
            
            if (contact == null)
            {
                contact.Lastname = "";

                Create(contact);

                contact = ContactData.GetAll().Except(contactList).First();
            }
           
            return contact;
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

        public void InitContactModification(ContactData contact)
        {
           driver.FindElement(By.XPath("(//a[@href='edit.php?id="+contact.Id+"'])")).Click();
        }

        public void InitContactDetails(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
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

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
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


        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].
                FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");          
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");            
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string bday = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string bmonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aday = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string amonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3,

                Middlename = middlename,

                Nickname =  nickname,  
                Title =     title,  
                Company = company,    
                
                Fax =   fax,      
                   
                Homepage = homepage,   
                Bday =     bday,   
                Bmonth =   bmonth,   
                Byear =   byear,    
                Aday = aday,
                Amonth = amonth,
                Ayear = ayear,
                Address2 = address2,
                Phone2 =phone2,
                Notes =notes
            };
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactDetails(index);
            string contactDetails = driver.FindElement(By.CssSelector("div#content")).Text;


            return new ContactData()
            {
                ContactDetails = contactDetails
            };
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupInFilter(group.Name);
            SelectContact(contact.Id);
            CommitRemovingContactFromGroup();
            manager.Navigator.GoToHomePage();
        }

        private void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectGroupInFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }
    }
}
