using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fair_Lottery__Version_2._0_
{
    interface TableObject
    {
        void Refresh();
    }
    class Persone : TableObject
    {
        int ID_Persone;
        public int sID_Persone { get { return ID_Persone; } }
        string Name;
        string Pass;
        decimal Money;
        private Persone(int ID_Persone, string Name, string Pass, decimal Money)
        {
            this.ID_Persone = ID_Persone;
            this.Name = Name;
            this.Pass = Pass;
            this.Money = Money;
        }
        public void Refresh()
        {
            Table.Persone.FindPersone(ID_Persone, out Name, out Pass, out Money);
        }
        public static Persone GetPersone(int ID_Persone)
        {
            string name;
            string pass;
            decimal money;
            Table.Persone.FindPersone(ID_Persone, out name, out pass, out money);
            return new Persone(ID_Persone, name, pass, money);
        }
        public static Persone CreatePersone(string Name, string Pass, decimal Money)
        {
            int id = Table.Persone.CreatePersone(Name, Pass, Money);
            return GetPersone(id);
        }
    }
}
