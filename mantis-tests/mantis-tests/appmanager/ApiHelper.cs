using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    public class ApiHelper : HelperBase
    {
        private string baseURL;

        public ApiHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
            
        }        

        public void IsExistAnyProject()
        {           
            if (!IsExistProjects())
            {
                #region Data
                AccountData account = new AccountData()
                {
                    Name = "administrator",
                    Password = "root"
                };
                DateTime now = DateTime.Now;
                ProjectData projectData = new ProjectData();
                projectData.Name = "testproject" + now.ToString("hhmmssddMMyyyy");
                #endregion

                CreateProject(account, projectData);
            }           
        }

        public bool IsExistProjects()
        {
            return GetProjectCount() > 0;
        }

        public int GetProjectCount()
        {
            return GetAll().Count();
        }

        public List<ProjectData> GetAll()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            List<ProjectData> projects = new List<ProjectData>();
            var mantisProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (var m in mantisProjects)
            {
                projects.Add(new ProjectData()
                {
                    Name = m.name,
                    Id = m.id
                });
            }                

            return projects;
        }

        public void CreateProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            client.mc_project_add(account.Name, account.Password, project);
        }
    }
}
