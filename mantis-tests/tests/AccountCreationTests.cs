using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.IO;
using TestContext = NUnit.Framework.TestContext;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("config_inc.php");
            using (Stream localFile = File.Open(TestContext.CurrentContext.TestDirectory
                +@"\config_inc.php", FileMode.Open))
            {
                app.Ftp.UploadFile("config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser6",
                Password = "password",
                Email = "testuser6@localhost.localdomain"
            };

            app.James.Delete(account);
            app.James.Add(account);
            app.Registration.Register(account);
        }
        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("config_inc.php");
        }
    }
}