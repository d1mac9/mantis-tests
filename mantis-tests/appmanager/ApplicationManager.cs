using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager : TestBase
    {
        protected IWebDriver driver;// Protected означает что "оно все еще внутреннее, но наследники тоже получают доступ"
        protected string baseURL;
        
        

         private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();//устанавливает соответствие между текущим потоком и объектом типа ApplicationManager




        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-1.2.17";
      
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);//   //Костыль - чтобы не падало при массовом запуске
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Project = new ProjectHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);
        }


        ~ApplicationManager() //Деструктор (тильдаКласс)
        {
            try
            {
                Driver.Quit();//Остановка браузера
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance() //static - глобальный. Можно будет обращаться как к ApplicationManager.GetInstance()
        {
            if (! app.IsValueCreated) //Если для текущего потока внутри этого хранилища ничего не создано, то создаем
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
                
            }
            return app.Value;
        }

        //http://localhost/mantisbt-1.2.17/admin/install.php


        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get;  set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public ProjectHelper Project { get; set; }
        public AdminHelper Admin { get; set;  }
        public APIHelper API { get;  set; }
    }
}
