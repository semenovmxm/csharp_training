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
        private const string ConfigPath = "/config_inc.php"; //"/config/config_inc.php";

        [TestFixtureSetUp]
        public void setUpConfig()
        {
            app.Ftp.BackUpFile(ConfigPath);
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload(ConfigPath, localFile);
            }
            
        }
  
        [Test]
        public void TestAccountRegistration()
        {
            DateTime now = DateTime.Now;
            AccountData account = new AccountData();

            account.Name = "testuser"+ now.ToString("hhmmssddMMyyyy");
            account.Password = "password";
            account.Email = account.Name +"@localhost.localdomain";

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);


        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackUpFile(ConfigPath);
        }
    }
}
