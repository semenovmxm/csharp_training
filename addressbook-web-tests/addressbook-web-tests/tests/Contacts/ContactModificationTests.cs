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
            contact.Lastname = "llll" ;
            contact.Middlename = "";// null; //"G1 " + DateTime.Now;
            contact.Firstname = "";// null; //"G1 " + DateTime.Now;
            #endregion

            app.Contacts.IfExistAnyContact();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[1].Lastname = contact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}