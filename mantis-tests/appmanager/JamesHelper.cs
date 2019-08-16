using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalisticTelnet;

namespace mantis_tests
{
    public class JamesHelper : HelperBase
    {
        public JamesHelper(ApplicationManager manager) : base(manager) { }

        public void Add(AccountData account)
        {
            if (Verify(account))//Если аккоунт создан, то нифига не делаем
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("adduser "+account.Name + " " + account.Password);//добавляем пользователя
            System.Console.Out.WriteLine(telnet.Read()); 

        }

        public void Delete (AccountData account)
        {
            if (! Verify(account))//Если аккоунт создан, то нифига не делаем
            {
                return;
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + account.Name);//добавляем пользователя
            System.Console.Out.WriteLine(telnet.Read());

        }



        public bool Verify(AccountData account)
        {
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + account.Name);//добавляем пользователя
            String s = telnet.Read();
            System.Console.Out.WriteLine(s);

            return ! s.Contains("does not exist");
        }

        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection("localhost", 4555);
            System.Console.Out.WriteLine(telnet.Read()); //Читаем текст, который James вывел на консоль и выводим на консоль
            telnet.WriteLine("root"); //Это вводится логин
            System.Console.Out.WriteLine(telnet.Read()); //Читаем текст, который James вывел на консоль и выводим на консоль
            telnet.WriteLine("root");//Это вводится пароль
            System.Console.Out.WriteLine(telnet.Read()); //Читаем текст, который James вывел на консоль и выводим на консоль
            return telnet;
        }

    }
}
