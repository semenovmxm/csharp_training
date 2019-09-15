using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackUpFile("/config/config_inc.php");
            using (Stream localFile = File.Open("/config/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config/config_inc.php", localFile);
            }
            
        }
  
        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };

            app.Registration.Register(account);


        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackUpFile("/config/config_inc.php");
        }
    }
}
