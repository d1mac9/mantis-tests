using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace mantis_tests
{
    public class ProjectHelper : HelperBase //Чтобы не дублировать OpenMainPage
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }
        //
        

        public void AddMantisProject(ProjectData project)
        {
            OpenManagePage();//Открываем страницу управления
            OpenManageProgectPage(); //Открыли страницу проектов
            InitProjectCreation();//тыкнули CreateNewProject
            FillNewProjectForm(project);//Заполнили форму проекта
            manager.Registration.SubmitOneButtonForm();//Тыкнули AddProject
        }

        public void FillNewProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr[1]/td/form/input[2]")).Click(); //Клик по кнопке Manage Projects
        }

        public void OpenManageProgectPage()
        {
            driver.FindElement(By.XPath("/html/body/div[2]/p/span[2]/a")).Click(); //Клик по кнопке Manage Projects
        }


        public void OpenManagePage()
        {
            driver.FindElement(By.XPath("/html/body/table[2]/tbody/tr/td[1]/a[7]")).Click(); //Клик по кнопке Manage

        }

        public void RemoveMantisProject(int N)
        {
            OpenManagePage();//Открываем страницу управления
            OpenManageProgectPage(); //Открыли страницу проектов
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr[" + (N+3) + "]/td[1]/a")).Click(); //тыкнули на выбранный проект
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            manager.Registration.SubmitOneButtonForm();
            
        }

        

        public List<ProjectData> GetProjectList()
        {
            OpenManagePage();//Открываем страницу управления
            OpenManageProgectPage(); //Открыли страницу проектов

            //ICollection<IWebElement> elements = driver.FindElements(By.)
            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElement(By.XPath("/html/body/table[3]/tbody"))
                .FindElements(By.XPath(".//tr[@class='row-1' or @class='row-2']"));


            foreach (IWebElement element in elements)
            {
                ProjectData project = new ProjectData(element.FindElement(By.XPath(".//td[1]")).Text, element.FindElement(By.XPath(".//td[5]")).Text);
                projects.Add(project);
            }

             return new List<ProjectData>(projects);
        }


    }
}

