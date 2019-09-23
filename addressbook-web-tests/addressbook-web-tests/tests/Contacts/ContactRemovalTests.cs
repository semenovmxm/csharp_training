using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest1()
        {
            app.Contacts.IfExistAnyContact();

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);

            //список контактов обновляется не сразу, поэтому ждем обновления
            int attempt = 0;
            bool isUpdateContactCount = false;
            while (!isUpdateContactCount && attempt < 10000)
            {
                isUpdateContactCount = oldContacts.Count - 1 == app.Contacts.GetContactCount();
                System.Threading.Thread.Sleep(2);
                attempt++;
            }

            Assert.IsTrue(isUpdateContactCount);

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactRemovalTest2()
        {
            app.Contacts.IfExistAnyContact();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);

            //список контактов обновляется не сразу, поэтому ждем обновления
            int attempt = 0;
            bool isUpdateContactCount = false;
            while ( !isUpdateContactCount && attempt < 10000)
            {
                isUpdateContactCount = oldContacts.Count - 1 == app.Contacts.GetContactCount();
                System.Threading.Thread.Sleep(2);
                attempt++;
            }

            Assert.IsTrue(isUpdateContactCount);

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
