using System;
//Создайте приложение калькулятор для перевода числа
//из одной системы исчисления в другую. Пользователь с помощью меню выбирает направление перевода. Например,
//из десятичной в двоичную. После выбора направления,
//пользователь вводит число в исходной системе исчисления.
//Приложение должно перевести число в требуемую систему. Предусмотреть случай выхода за границы диапазона,
//определяемого типом int, неправильный ввод.

//Вообще мне кажется лучше было бы сделать через массив или строку, но
//насколько я понял из условия(последнее предложение) нужно делать в формате int
//вообще классно было бы делать через строку, тк там можно будет исмпользовать не только 0-9, а и A-Z для систем счисления 10+

namespace MyFirstMac
{
    class Program
    {

        static int PersonInput() //ну почти главный цикл
        {
            int ten = 0, num=0;
            byte base1=0, base2=0;  //Надеюсь это не задачка с олимпиады,
                                    //где меня просили переводить число во все
                                    //системы счисления от 1 до 10^18, поэтому надеюсь 255 хватит
                                    //думаю в основном нужно будет только 1-16

            do
            {
                try //проверка всех входных данных
                {
                    Console.Write("Enter first number: ");
                    num = checked(Convert.ToInt32(Console.ReadLine()));
                    Console.Write("Enter current base of the number system: ");
                    base1 = checked(Convert.ToByte(Console.ReadLine()));
                    Console.Write("Enter target base of the number system: ");
                    base2 = checked(Convert.ToByte(Console.ReadLine()));
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Too large number!");
                }

                ten = BaseToTen(num, base1); //здесь же будет проверка на соответствие числа и основания системы
            } while (ten == 0); //Если ошибка ввода - повторяется

            return Resoult(ten, base2);
        }

        static int BaseToTen(int num, byte base1) //перевод в десятичную
        {
            int ten = 0;

            for (int i = 0; num > 0; i++)
            {
                if (num % 10 > base1 - 1) //проверка на соответствие числа и основания системы
                {
                    Console.WriteLine("Number and base mismatch!");
                    return 0;
                }

                try //переполнение (например из AAAAAAAAA в 45812984490)
                {
                    ten = checked(ten + num % 10 * (int)(Math.Pow(base1, i)));
                    num = num / 10;
                }
                catch (System.OverflowException e)
                {
                    Console.WriteLine("Too large number!");
                    return 0;
                }
            }

            return ten;
        }

        static int Resoult(int ten, byte base2) //перевод из десятичной в нужную
        {
            int res = 0;

            try
            {
                for (int i = 0; ten > 0; i++)
                {
                    res = checked(res + ten % base2 * (int)(Math.Pow(10, i))); //если будет переполнение(например из 699050 в 10101010101010101010)
                    ten /= base2;
                }

                Console.WriteLine("New number: " + res); //Если переполнение - не напечатается
            } catch (System.OverflowException e)
            {
                Console.WriteLine("Too large number!");
            }

            return res;
        }

        static void Main(string[] args)
        {
            int res;
            char key;
            byte base2; 

            do //пока пользователь хочет продолжать
            {
                res = PersonInput();    
    
                Console.Write("Press 1 to repeat: ");
                key = Convert.ToChar(Console.ReadKey().Key);

            } while (key == '1');
        }
    }
}

