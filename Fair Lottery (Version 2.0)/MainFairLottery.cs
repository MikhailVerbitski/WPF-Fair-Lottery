using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Lottery__Version_2._0_
{
    class Pair<T1, T2>
    {
        private T1 first;
        private T2 second;
        public T1 First { get { return first; } set { first = value; } }
        public T2 Second { get { return second; } set { second = value; } }
        public Pair() { }
        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }
    }
    class MainFairLottery
    {
        static void Main()
        {
            Table.ConnectWithDataBase();

            Persone persone = Persone.GetPersone(1);
            Game dice = new Dice();
            dice.Run(persone);

            Console.ReadKey();

            Table.DisconnectWithDataBase();
        }
    }
}
