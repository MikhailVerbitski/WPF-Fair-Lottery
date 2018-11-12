using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fair_Lottery.Logic
{
    [Serializable]
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
    [Serializable]
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
        public void Serializ(string Way)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream file = new FileStream(Way, FileMode.CreateNew))
                binaryFormatter.Serialize(file, this);
        }
        public static Persone Deserializ(string Way)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Persone persone;
            using (FileStream file = new FileStream(Way, FileMode.Open))
                persone = binaryFormatter.Deserialize(file) as Persone;
            Logic.Table.Persone.UpdateMoney(persone.ID, persone.Money - Table.Persone.GetMoney(persone.ID));
            return persone;
        }
    }
    class Guest : Player
    {
        public Guest(int ID_Guest = -42, string Name = "Гость", decimal Money = 100) : base(ID_Guest, Name, Money) { }
    }
}