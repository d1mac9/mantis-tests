using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{

    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void SetupConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open(TestContext.CurrentContext.TestDirectory  + "/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);

            }
            
        }

        /*
        Перед тестом запустить Xampp -> FileZilla (проверить, что создана группа (справа)
        и что SharedFolders смотрит на D:\programs\xampp\htdocs\mantisbt-1.2.17
        Запустить D:\programs\james\james-2.3.1\bin\run.bat
        */
        [Test]
        public void TestAccountRegistration()
        {
            
            AccountData account = new AccountData()
            {
                Name = "testuser5",
                Password = "password",
                Email = "testuser5@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccount();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            //лямбда выражение, которое вернёт тру или фолз в зависимости от того, подходит элемент под нужные условия или не подходит
            //ищем предикат, у которого Name = 

            if(existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }


            //часть ниже остается в качестве домашнего задания
            

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
            
            //
         }

        [TearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
        //D:\programs\xampp\htdocs\mantisbt-1.2.17 - тут лежит config_inc.php

    }
}
