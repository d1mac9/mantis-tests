using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        public static bool PREFORM_LONG_UI_CHECKS = true; //Проверки в TearDown которые жертвуют временем ради надежности
        public ApplicationManager app;

        [SetUp]

        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
            
        }

        public static Random rnd = new Random();//генератор случайных чисел
        

        public static string GenerateRandomString(int max)
        {
            
            //Дальше делаем число в диапазоне от 0 до max
            int l = Convert.ToInt32(rnd.NextDouble() * max);//
                                                            //NextDouble() генерирует число от 0 до 1
                                                            //rnd.NextDouble() * max = число в диапазоне от 0 до max

            StringBuilder builder = new StringBuilder();
            for (int i=0; i<l; i++) //генерируем l разнличных символов
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65))); //так шанс на успешность выше :)
                
                //попробую +64 и *90
                //было так в лекции builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223))); //+32 Потому что в таблице ANCII символы до 32 - непечатные
                //случайное число в диапазоне от 32 до 223
            }
            return builder.ToString();
        }
    } 
}
