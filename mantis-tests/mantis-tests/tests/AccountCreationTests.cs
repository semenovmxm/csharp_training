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
        public void TestTheSameAccountRegistration()
        {            
            List<AccountData> accounts = app.Admin.GetAllAccounts();

            AccountData account = new AccountData();
            account.Name = "testuser1";
            account.Password = "password";
            account.Email = account.Name +"@localhost.localdomain";

            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }            

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [Test]
        public void TestTheSameAccountRegistration2()
        {
            app.Auth.Login(new AccountData("administrator", "root"));
            List<AccountData> accounts = app.Admin.GetAllAccounts();

            AccountData account = new AccountData();
            account.Name = "testuser2";
            account.Password = "password";
            account.Email = account.Name + "@localhost.localdomain";

            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Registration.DeleteAccount(existingAccount);
            }
            app.Auth.Logout();

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }
        [Test]
        public void TestAccountRegistration()
        {
            DateTime now = DateTime.Now;
            AccountData account = new AccountData();

            account.Name = "testuser" + now.ToString("hhmmssddMMyyyy");
            account.Password = "password";
            account.Email = account.Name + "@localhost.localdomain";                      

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
