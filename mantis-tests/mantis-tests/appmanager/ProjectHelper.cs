using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }


        public void RemoveProject(int index)
        {
            manager.ManagementMenu.GoToManagementProjectPage();
            GoToProjectModificationPage(index);
            SubmitProjectRemoveing();
            ConfirmProjectRemoving();
        }        

        public void IfExistAnyProject()
        {
            manager.ManagementMenu.GoToManagementProjectPage();

            if (!IsExistRecords())
            {
                DateTime now = DateTime.Now;
                ProjectData project = new ProjectData();
                project.Name = "testproject" + now.ToString("hhmmssddMMyyyy");

                CreateProject(project);
            }
        }
        private void ConfirmProjectRemoving()
        {
            driver.FindElement(By.XPath("//input[4]")).Click();
        }

        private void SubmitProjectRemoveing()
        {
            driver.FindElement(By.XPath("//input[3]")).Click();            
        }

        private void GoToProjectModificationPage(int index)
        {
            driver.FindElement(By.XPath("//td/a[" + index + "]")).Click();
        }

        public void CreateProject(ProjectData project)
        {   
            manager.ManagementMenu.GoToManagementProjectPage();
            InitNewProjectCreation();
            FillNewProjectForm(project);
            SubmitProjectCreation();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        private void FillNewProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        private void InitNewProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }


        private void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.22.0/login_page.php"; 
        }
    }
}
