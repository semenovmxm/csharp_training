using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AurhTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            #region ContactDataRegion
            ContactData contact = new ContactData();
            contact.Firstname = "";
            contact.Middlename = "";
            contact.Lastname = "qqq";

            #endregion

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
