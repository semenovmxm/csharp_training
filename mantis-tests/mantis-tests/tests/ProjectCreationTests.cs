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
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            DateTime now = DateTime.Now;
            ProjectData project = new ProjectData();
            project.Name = "testproject" + now.ToString("hhmmssddMMyyyy");




            var oldProjects = app.API.GetAll();

            app.Project.CreateProject(project);

            var newProjects = app.API.GetAll();
            int num = newProjects.Count - 1;

            Assert.AreEqual(oldProjects.Count + 1, newProjects.Count);
            project.Id = newProjects[num].Id;
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }

}
}
