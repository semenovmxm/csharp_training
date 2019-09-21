using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoveTest()
        {
            app.Project.IfExistAnyProject();
            app.Project.RemoveProject(1);
        }

    }
}
