using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            #region ContactDataRegion
            ContactData newData = new ContactData();
            newData.Lastname = "1234" ;
            newData.Middlename = "";// null; //"G1 " + DateTime.Now;
            newData.Firstname = "";// null; //"G1 " + DateTime.Now;
            #endregion

            app.Contacts.IfExistAnyContact();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];
            newData.Id = oldData.Id;

            app.Contacts.Modify(newData);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }
        }
    }
}