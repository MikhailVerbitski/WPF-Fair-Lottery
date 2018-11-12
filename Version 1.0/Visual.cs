using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0
{
    static class Visual
    {
        static string Key = "LuckyGuy";
        public static void Welcome()
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в Fair Lottery (Честная лотерея version 1.0!)");
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить.");
            Console.ReadKey(true);
        }
        public static bool EntranceOrRegistration()
        {
            Console.Clear();
            Console.WriteLine("1 - Регистрация\n2 - Вход");
            return Console.ReadKey(true).KeyChar == '1' ? true : false;
        }
        public static Persona Registration()
        {
            Console.Clear();
            Console.WriteLine("Введите пожалуйста логин:");
            string Name = Console.ReadLine();
            Console.WriteLine("Введите пожалуйста пароль:");
            string Pass = Console.ReadLine();
            decimal money = 100;
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
                        money = Convert.ToDecimal(str[1]);
                    }
                    else
                        Console.WriteLine("Ключ не подошел =(");
                }
            } while (k == '1');
            return DataBase.CreatePersona(Name, Pass, money);
        }
        public static Persona Entrance()
        {
            throw new Exception();
        }
        public static void Select(Game game, Persona persona, bool selected = true, bool bet = true)
        {
            Console.WriteLine();
            int i = 0;
            if(selected)
            {
                Console.WriteLine("Сделайте своей выбор(от 1 до " + game.CountItems + ")");
                i = Convert.ToInt32(Console.ReadLine()) - 1;
                game.Select(i);
            }
            if(bet)
            {
                Console.WriteLine("Сделайте ставку");
                game.MakeBet(persona,i,Convert.ToInt32(Console.ReadLine()));
            }
        }
        public static int SelectGame(Persona persona)
        {
            Console.Clear();
            Console.WriteLine("Выберите игру");
            Console.WriteLine("1 - кубик");
            Console.WriteLine("2 - стаканчик");
            Console.WriteLine("3 - лотерея");
            return Convert.ToInt32(Console.ReadLine());
        }
        public static void ShowGame(Game game)
        {
            Console.Clear();
            for(int i = 0; i < game.CountItems; i++)
                Console.Write(game[i].ShowItem + " | ");
            Console.WriteLine();
        }
        public static void ShowResult(Game game, int WinNumb)
        {
            Console.WriteLine();
            if(WinNumb >= 0)
                Console.WriteLine("Победил элемент " + game[game.GetWinNumber(WinNumb)].ShowItem);
            Console.WriteLine("Выйгрыш составляет: " + game.SumBet);
        }
        public static bool StillPlay(Game game, Persona persona)
        {
            Console.WriteLine("\n" + "Игра окончена");
            Console.WriteLine("Исход игры: " + game.Result);
            Console.WriteLine("Ваш баланс:" + persona.Money);
            Console.WriteLine("Введите 1 чтобы играть дальше!");
            return Console.ReadKey(true).KeyChar == '1'; 
        }
        public static bool PlayAgain()
        {
            Console.Clear();
            Console.WriteLine("Сыграем еще во что-нибудь?(1 - да)");
            return Console.ReadKey(true).KeyChar == '1';
        }
        public static bool PlayFurther()
        {
            Console.WriteLine("Играем дальше?(1 - да)");
            return Console.ReadKey(true).KeyChar == '1';
        }
        public static void YouLoss()
        {
            Console.WriteLine("Вы проиграли!");
        }
    }
}
