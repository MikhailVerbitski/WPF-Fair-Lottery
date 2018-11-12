using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Logic
{   
    static class Table
    {
        static SQLiteConnection DBConnect = new SQLiteConnection(@"Data Source=..\..\..\DataBase\DataBase.db");
        static SQLiteDataReader reader;
        public static void ConnectWithDataBase()
        {
            DBConnect.Open();
        }
        public static void DisconnectWithDataBase()
        {
            DBConnect.Close();
        }
        public static class Bet
        {
            public static int CreateBet(int ID_of_Item, int ID_of_Raffle, decimal Size, int NumberItem)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "INSERT INTO Bet(ID_of_Item, ID_of_Raffle, Size, Number_Items) VALUES (@ID_of_Item, @ID_of_Raffle, @Size, @Number_Items);" +
                                        "SELECT last_insert_rowid() AS ID_Bet FROM Bet LIMIT 1;";
                command.Parameters.Add("@ID_of_Item", System.Data.DbType.Int32).Value = ID_of_Item;
                command.Parameters.Add("@ID_of_Raffle", System.Data.DbType.Int32).Value = ID_of_Raffle;
                command.Parameters.Add("@Size", System.Data.DbType.Decimal).Value = Size;
                command.Parameters.Add("@Number_Items", System.Data.DbType.Int32).Value = NumberItem;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static int GetNumber(int ID_Bet)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Number_items FROM Bet WHERE ID_Bet=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Bet;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static decimal GetSize(int ID_Bet)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Size FROM Bet WHERE ID_Bet=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Bet;
                return Convert.ToDecimal(command.ExecuteScalar());
            }
            public static int GetID_of_Items(int ID_Bet)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT ID_of_Item FROM Bet WHERE ID_Bet=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Bet;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static int GetID_of_Raffle(int ID_Bet)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT ID_of_Raffle FROM Bet WHERE ID_Bet=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Bet;
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public static class Items
        {
            public static int CreateItems(string Name, string NameOwner)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText =   "INSERT INTO Items(Name, NameOwner) VALUES (@Name, @NameOwner);" +
                                        "SELECT last_insert_rowid() AS ID_Items FROM Items LIMIT 1;";
                command.Parameters.Add("@ID_of_Item", System.Data.DbType.String).Value = Name;
                command.Parameters.Add("@ID_of_Raffle", System.Data.DbType.String).Value = NameOwner;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static int GetCountInGame(string NameGame)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Items WHERE Name_owner=@namegame";
                command.Parameters.Add("@namegame", System.Data.DbType.String).Value = NameGame;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static Pair<int,string>[] GetNames(string GameName)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Name, ID_Items FROM Items WHERE Name_owner=@namegame";
                command.Parameters.Add("@namegame", System.Data.DbType.String).Value = GameName;
                reader = command.ExecuteReader();
                Pair<int, string>[] Items = new Pair<int, string>[GetCountInGame(GameName)];
                for(int i=0; i< Items.Length; i++)
                {
                    reader.Read();
                    Items[i] = new Pair<int, string>(Convert.ToInt32(reader["ID_Items"]), Convert.ToString(reader["Name"]));
                }
                return Items;
            }
            public static string GetNameItem(int ID_Items)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Name FROM Items WHERE ID_Items=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Items;
                return Convert.ToString(command.ExecuteScalar());
            }
        }
        public static class Persone
        {
            public static int CreatePersone(string Name, string Pass, decimal Money)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText =   "INSERT INTO Persone(Name, Pass, Money) VALUES (@Name, @Pass, @Money);" +
                                        "SELECT last_insert_rowid() AS ID_Persone FROM Persone LIMIT 1;";
                command.Parameters.Add("@ID_of_Item", System.Data.DbType.String).Value = Name;
                command.Parameters.Add("@ID_of_Raffle", System.Data.DbType.String).Value = Pass;
                command.Parameters.Add("@Size", System.Data.DbType.Decimal).Value = Money;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static void FindPersone(int ID_Persone, out string Name, out string Pass, out decimal Money)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT * FROM Persone WHERE ID_Persone=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Persone;
                reader = command.ExecuteReader();
                reader.Read();
                Name = Convert.ToString(reader["Name"]);
                Pass = Convert.ToString(reader["Pass"]);
                Money = Convert.ToDecimal(reader["Money"]);
            }
            public static bool CheckPersone(string Name)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Persone WHERE Name=@name";
                command.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            public static int FindPersone(string Name, string Pass)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT ID_Persone FROM Persone WHERE Name=@Name AND Pass=@Pass";
                command.Parameters.Add("@Name", System.Data.DbType.String).Value = Name;
                command.Parameters.Add("@Pass", System.Data.DbType.String).Value = Pass;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static void UpdateMoney(int ID_Persone, decimal Result)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "UPDATE Persone SET Money=Money+@result WHERE ID_Persone=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Persone;
                command.Parameters.Add("@result", System.Data.DbType.Decimal).Value = Result;
                command.ExecuteNonQuery();
            }
        }
        public static class Raffle
        {
            public static int CreateRaffle(int ID_of_Persone, int WinNum)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "INSERT INTO Raffle(ID_of_Persone, Win_Number) VALUES (@ID_of_Persone, @Win_Number);" +
                                        "SELECT last_insert_rowid() AS ID_Raffle FROM Raffle LIMIT 1;";
                command.Parameters.Add("@ID_of_Persone", System.Data.DbType.Int32).Value = ID_of_Persone;
                command.Parameters.Add("@Win_Number", System.Data.DbType.Int32).Value = WinNum;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static void SetResult(int ID_Raffle, decimal Result)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "UPDATE Raffle SET Result=@result WHERE ID_Raffle=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Raffle;
                command.Parameters.Add("@result", System.Data.DbType.Decimal).Value = Result;
                command.ExecuteNonQuery();
            }
            public static int GetWinNumber(int ID_Raffle)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Win_Number FROM Raffle WHERE ID_Raffle=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Raffle;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static decimal GetResult(int ID_Raffle)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Result FROM Raffle WHERE ID_Raffle=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Raffle;
                return Convert.ToDecimal(command.ExecuteScalar());
            }
        }
    }
}