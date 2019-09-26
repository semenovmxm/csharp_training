using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ConactDetailsTests : AuthTestBase
    {

        [Test]
        public void TestConactDetails()
        {
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            var fd = fromDetails.ContactDetails;
            var ff = fromForm.ContactDetails;


            Assert.AreEqual(fd, ff);
        }
    }
}
