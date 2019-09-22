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

        [Test]
        public void ProjectRemoveTestApiVerify()
        {
            app.API.IsExistAnyProject();

            
            var oldProjects = app.API.GetAll();
            var toBeRemoved = oldProjects[0];

            app.Project.RemoveProject(1);

            Assert.AreEqual(oldProjects.Count - 1, app.API.GetProjectCount());

            var newProjects = app.API.GetAll();

            oldProjects.RemoveAt(0);
            Assert.AreEqual(oldProjects, newProjects);

            foreach (var project in newProjects)
            {
                Assert.AreNotEqual(project.Id, toBeRemoved.Id);
            }
        }

    }
}
