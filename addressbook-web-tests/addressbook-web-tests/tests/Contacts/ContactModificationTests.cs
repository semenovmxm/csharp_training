using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AurhTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            #region ContactDataRegion
            ContactData contact = new ContactData();
            contact.Firstname = "G1 " + DateTime.Now;
            contact.Middlename = null; //"G1 " + DateTime.Now;
            contact.Lastname = null; //"G1 " + DateTime.Now;
            //contact.Nickname = "G1 " + DateTime.Now;
            //contact.Title = "G1 " + DateTime.Now;
            //contact.Company = "G1 " + DateTime.Now;
            //contact.Address = "G1 " + DateTime.Now;
            //contact.Home = "G1 " + DateTime.Now;
            //contact.Mobile = "G1 " + DateTime.Now;
            //contact.Work = "G1 " + DateTime.Now;
            //contact.Fax = "9876543210 ";
            //contact.Email = "G1 " + DateTime.Now;
            //contact.Email2 = "G1 " + DateTime.Now;
            //contact.Email3 = "G1 " + DateTime.Now;
            //contact.Homepage = "G1 " + DateTime.Now;
            //contact.Bday = "22";
            //contact.Bmonth = "January";
            //contact.Byear = "2000";
            //contact.Aday = "10";
            //contact.Amonth = "January";
            //contact.Ayear = "2000";
            //contact.New_group_id = "7";
            //contact.New_group = "e";
            //contact.Address2 = "G1 ";
            //contact.Phone2 = "9876543210";
            //contact.Notes = "G1 " + DateTime.Now;
            contact.Index = "10";
            #endregion

            app.Contacts.
                IfExistAnyContact().
                Modify(contact);
        }
    }
}