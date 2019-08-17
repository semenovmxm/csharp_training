using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AurhTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.IfExistAnyContact();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove();

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(1); 
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
