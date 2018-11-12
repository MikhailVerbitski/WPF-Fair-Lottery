using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0
{
    [Serializable]
    class Persona
    {
        string Name;
        string Password;
        public decimal Money { get; set; }
        public Stack<Game> HistoryGames;
        public Persona(string Name, string Password, decimal Money)
        {
            this.Name = Name;
            this.Password = Password;
            this.Money = Money;
            HistoryGames = new Stack<Game>();
        }
        public void Play()
        {
            do
            {
                bool running = false;
                int gameCount = -1;
                do
                {
                    if (!running)
                        gameCount = Visual.SelectGame(this);
                    Game g = GamesFactory(gameCount);
                    running = g.Run(this);
                    HistoryGames.Push(g);
                } while (running);
            } while (Visual.PlayAgain());
        }
        public static Game GamesFactory(int i)
        {
            switch(i)
            {
                case 1:
                    return new Dice();
                case 2:
                    return new Cups();
                //case 3:
                //return new Lottery();
                default:
                    throw new Exception();
            }
        }
    }
    class Pair<T1, T2>
    {
        public T1 first { get; set; }
        public T2 second { get; set; }
        public Pair() { }
        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }
    }
    struct Item
    {
        public string Name;
        public string ShowItem;
        public decimal Bet;
        public bool Selected;
        public Item(string Name, string ShowItem, decimal Bet, bool Selected)
        {
            this.Name = Name;
            this.ShowItem = ShowItem;
            this.Bet = Bet;
            this.Selected = Selected;
        }
    }
}