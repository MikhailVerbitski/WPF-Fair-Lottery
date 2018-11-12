using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Lottery__Version_2._0_
{
    class Visual
    {
        static string Key = "LuckyGuy";
        public static void Welcome()
        {
            Console.WriteLine("Добро пожаловать в Fair Lottery (Честная лотерея version 1.0!)");
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить.");
            Console.ReadKey(true);
            Console.Clear();
        }
        public static bool EntranceOrRegistration()
        {
            Console.WriteLine("1 - Регистрация\n2 - Вход");
            return (Console.ReadKey(true).KeyChar == '1') ? true : false;
        }
        public static void Registration(out string Name, out string Pass, out decimal Money)
        {
            Console.WriteLine("Введите пожалуйста логин:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите пожалуйста пароль:");
            Pass = Console.ReadLine();
            Money = 100;
            char k;
            Console.Clear();
            Console.WriteLine("Если у вас есть ключ на изменение суммы денег введите его в формате: Ключ-Деньги");
            do
            {
                Console.WriteLine("Будете вводить ключ?\n1 - да\n2 - нет");
                k = Console.ReadKey(true).KeyChar;
                if (k == '1')
                {
                    Console.WriteLine("Вводите ключ:");
                    string[] str = Console.ReadLine().Split('-');
                    if (str[0] == Key)
                    {
                        Console.WriteLine("Ключ принят!");
                        Money = Convert.ToDecimal(str[1]);
                    }
                    else
                        Console.WriteLine("Ключ не подошел =(");
                }
            } while (k == '1');
            Console.Clear();
        }
        public static void Authentication(out string Name, out string Pass)
        {
            Console.WriteLine("Введите логин:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            Pass = Console.ReadLine();

        }
        public static void FailAuthentication()
        {
            Console.WriteLine("Пользователь не найден!\nВведите данные заново:");
        }
        public static void FailRegistration()
        {
            Console.WriteLine("Пользователь с таким логином уже существует!");
        }
        public static Pair<int,int> ShowAndSelectItemDice()
        {
            int count = Table.Items.GetCountInGame("Dice");
            Pair<int,string>[] Items = Table.Items.GetNames("Dice");
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    Console.Write(", ");
                Console.Write(Items[i].Second);
            }
            Console.WriteLine("\nВыберите элемент:");
            int num = Convert.ToInt32(Console.ReadLine());
            return new Pair<int, int>(Items[num - 1].First, num);
        }
        public static Pair<int, int> ShowAndSelectItemLottery(int count, decimal price)
        {
            Console.WriteLine();
            Console.WriteLine("Выможете вводить номера билетов, а можете покупать случайный(1|2)");
            bool b = false;
            do
            {
                switch (Convert.ToInt32(Console.ReadKey().KeyChar))
                {
                    case 1:
                        bool b1 = false;
                        do
                        {
                            Console.WriteLine("Введиет номер билета который вы хотите купить от 1 до " + count);


                        } while (b1);
                        b = false;
                        break;
                    case 2:
                        b = false;
                        break;
                    default:
                        Console.WriteLine("Такой вариант не допустим! Введите 1 или 2");
                        break;
                        b = true;
                }
            } while (b);
        }
        public static decimal MakeBet()
        {
            Console.WriteLine("Сделайте ставку");
            return Convert.ToDecimal(Console.ReadLine());
        }
        public static void ShowResult(int ID_Bet, string NameGame)
        {
            int ID_of_Raffle = Table.Bet.GetID_of_Raffle(ID_Bet);
            Console.WriteLine("Результаты:");
            Console.WriteLine("Вы поставили на: " + Table.Items.GetNameItem(Table.Bet.GetID_of_Items(ID_Bet)));
            Console.WriteLine("Выйграл элемент: " + Table.Items.GetNames(NameGame)[Table.Raffle.GetWinNumber(ID_of_Raffle) - 1].Second);
            Console.WriteLine("Результат вашей игры: " + Table.Raffle.GetResult(ID_of_Raffle));
        }
    }
}
