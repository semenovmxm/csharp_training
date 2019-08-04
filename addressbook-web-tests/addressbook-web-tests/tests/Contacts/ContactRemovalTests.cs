using System;
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
            #region ContactDataRegion
            ContactData contact = new ContactData();
            contact.Index = "10";
            #endregion

            app.Contacts.Remove(contact);
        }
    }
}
