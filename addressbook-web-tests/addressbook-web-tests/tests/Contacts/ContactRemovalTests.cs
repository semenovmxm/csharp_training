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
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove();

            //список контактов обновляется не сразу, поэтому ждем обновления
            int attempt = 0;
            while (oldContacts.Count - 1 != app.Contacts.GetContactCount() 
                && attempt < 30)
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            }

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

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
