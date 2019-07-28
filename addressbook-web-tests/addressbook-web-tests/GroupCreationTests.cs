﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            AccountData admin = new AccountData();
            admin.Username = "admin";
            admin.Password = "secret";

            GroupData group = new GroupData();
            group.Name = "G1 " + DateTime.Now;
            group.Header = "H1 " + DateTime.Now;
            group.Footer = "F1 " + DateTime.Now;

            GoToHomePage();
            Login(admin);
            GoToGroupsPage();
            InitNewGroupCreation();            
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }

       
    }
}
