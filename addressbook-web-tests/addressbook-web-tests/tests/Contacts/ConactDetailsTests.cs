﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ConactDetailsTests : AurhTestBase
    {

        [Test]
        public void TestConactDetails()
        {
            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
           
            Assert.AreEqual(fromDetails.ContactDetails, fromForm.ContactDetails);
        }
    }
}