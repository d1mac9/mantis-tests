using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpaqueMail;


namespace mantis_tests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager manager) : base(manager) { }

        public string GetLastMail(AccountData account)//читаем последнее письмо и возвращаем текст
        {
            for (int i = 0; i < 30; i++) 
            {
                Pop3Client pop3 = new Pop3Client("localhost", 110, account.Name, account.Password, false);
                pop3.Connect();
                pop3.Authenticate();

                if (pop3.GetMessageCount() > 0)
                {
                    MailMessage message = pop3.GetMessage(1);
                    //ReadOnlyMailMessage message = pop3.GetMessage(1); //устарело!!

                    string body = message.Body;
                    pop3.DeleteMessage(1);
                    pop3.LogOut();
                    return message.Body;
                    
                }
                else
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }

            return null; //Если цикл завершился возвращаем ничего - письмо так и не пришло
        }






    }
}
