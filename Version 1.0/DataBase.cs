using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Version_1._0
{
    class DataBase
    {
        private SQLiteConnection dataB = new SQLiteConnection(@"F:\...");
        DataBase()
        {
            dataB.Open();
        }
        ~DataBase()
        {
            dataB.Close(); 
        }

        public static Persona CreatePersona(string Name, string PassWord, decimal money)
        {
            return new Persona(Name, PassWord, money);
        }
    }

}
