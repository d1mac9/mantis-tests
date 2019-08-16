using System;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class UnitTest1 : TestBase
    {
        [Test]
        public void TestMethod1()
        {
            AccountData account = new AccountData()
            {
                Name = "xxx",
                Password = "yyy"
            };

            Assert.IsFalse(app.James.Verify(account)); //Проверяем, что такого пользователя нет

            app.James.Add(account); //Добавляем
            Assert.IsTrue(app.James.Verify(account)); //Проверяем, что теперь такой пользователь есть

            app.James.Delete(account); //Удаляем

            Assert.IsFalse(app.James.Verify(account)); //Проверяем, что такого пользователя нет

        }
    }
}
