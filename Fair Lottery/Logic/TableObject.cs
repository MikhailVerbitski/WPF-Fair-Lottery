using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Persone
    {
        int ID_Persone;
        string Name;
        string Pass;
        decimal Money;

        public int sID_Persone { get { return ID_Persone; } }
        public string GetName { get { return Name; } }

        public Persone(int ID_Persone, string Name, string Pass, decimal Money)
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
    }
}
