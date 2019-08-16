using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class MantisProjectTests : TestBase

    {

        /*
        Перед тестом запустить Xampp -> FileZilla (проверить, что создана группа (справа)
        и что SharedFolders смотрит на D:\programs\xampp\htdocs\mantisbt-1.2.17
        Запустить D:\programs\james\james-2.3.1\bin\run.bat
        ЗАДАВАТЬ НОВЫЕ ДАННЫЕ!!!!
        */
        [SetUp] //Тут нужно выполнить логин как Администратор

        public void LoginAsAdmin()
        {
            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };
            app.Registration.OpenMainPage(); //Открыли форму
            app.Registration.FillAuthForm(admin); //Заполнили форму аутентификации
            app.Registration.SubmitOneButtonForm();//Тыкнули кнопку Login

        }



        [Test]
        public void MantisProjectAdding()
        {
            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };
            //List<ProjectData> oldprojects = new List<ProjectData>();//Получение списка через интерфейс
            //oldprojects = app.Project.GetProjectList();
            List<ProjectData> oldprojects2 = new List<ProjectData>(); //Получение списка через API
            oldprojects2 = app.API.APIGetProjectList(admin);

            ProjectData project = new ProjectData
            {
                Name = "112",
                Description = "112"
            };

            app.Project.AddMantisProject(project);

            oldprojects2.Add(project);
            
            List<ProjectData> newprojects = app.API.APIGetProjectList(admin);
            oldprojects2.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects2, newprojects);

        }

        [Test]
        public void MantisProjectRemoving()
        {
            /*AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            }; */
            int N = 3;//ВВОДИМ САМИ Порядковый номер удаляемого контакта начиная с НУУУУУЛЯЯЯ!!!

            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();
            //oldprojects = app.API.APIGetProjectList(admin);
            if (oldprojects.Count == 0)
            {
                ProjectData project = new ProjectData
                {
                    Name = "555",
                    Description = "555"
                };

                //app.API.APIAddMantisProject(admin, project);
                app.Project.AddMantisProject(project);
                oldprojects = app.Project.GetProjectList();
                //oldprojects = app.API.APIGetProjectList(admin);
                N = 0;
            }
            else if(oldprojects.Count < N)
            {
                N = oldprojects.Count - 1;
            }

            ProjectData removedProject = oldprojects[N]; 

            app.Project.RemoveMantisProject(N);

            oldprojects.Remove(removedProject);
            //List<ProjectData> newprojects = app.API.APIGetProjectList(admin);
            List<ProjectData> newprojects = app.Project.GetProjectList();
            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);

        }




        [TearDown] //Тут нужно делать logout

        public void Loguot()
        {
            app.Registration.InitLogOut();
        }


    }
}
