using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Lottery.Logic
{
    abstract class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }

        public Player(int ID, string Name, decimal Money)
        {
            this.ID = ID;
            this.Name = Name;
            this.Money = Money;
        }
        public static Player GetPlayer()
        {
            return new Guest();
        }
        public static Player GetPlayer(bool AuthorizationOrRegistration, string Name, string Pass)
        {
            if(AuthorizationOrRegistration)
            {
                int ID = Logic.Table.Persone.FindPersone(Name, Pass);
                decimal Money = Logic.Table.Persone.GetMoney(ID);
                return new Persone(ID, Name, Pass, Money);
            }
            else
            {
                if (Logic.Table.Persone.CheckPersone(Name))
                    throw new Exception("Пользователь уже существует");
                int ID = Logic.Table.Persone.CreatePersone(Name, Pass);
                decimal Money = Logic.Table.Persone.GetMoney(ID);
                return new Persone(ID, Name, Pass, Money);
            }
        }
    }
    class Persone : Player
    {
        private string Pass;

        public Persone(int ID_Persone, string Name, string Pass, decimal Money = 100) : base(ID_Persone, Name, Money)
        {
            this.Pass = Pass;
        }
        public void Refresh()
        {
            string Name;
            decimal Money;
            Table.Persone.FindPersone(ID, out Name, out Pass, out Money);
            this.Name = Name;
            this.Money = Money;
        }
    }
    class Guest : Player
    {
        public Guest(int ID_Guest = -42, string Name = "Гость", decimal Money = 100) : base(ID_Guest, Name, Money) { }
    }
}