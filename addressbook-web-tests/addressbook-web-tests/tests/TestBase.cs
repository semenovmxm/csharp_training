using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();

            #region AccountDataRegion
            AccountData admin = new AccountData();
            admin.Username = "admin";
            admin.Password = "secret";
            #endregion

            app.Navigator.GoToHomePage();
            app.Auth.Login(admin);
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }
    }
}
