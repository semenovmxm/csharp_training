using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            AccountData admin = new AccountData();
            admin.Username = "admin";
            admin.Password = "secret";

            GroupData group = new GroupData();
            group.Index = "1";

            GoToHomePage();
            Login(admin);
            GoToGroupsPage();
            SelectGroup(group);
            RemoveGroup();
            ReturnToGroupsPage();
            Logout();
        }

        
    }
}
