using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {

        [Test]
        public void AddNewIssues()
        {           
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Id = "13"
            };

            IssueData issueData = new IssueData()
            {
                Summary = "some short text",
                Description = "some long text",
                Category = "General"
            };

            app.API.CreateNewIssue(account, project, issueData);
        }

    }
}
