using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace Fair_Lottery.Logic
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
            public static void CreateBet(int ID_of_Item, int ID_of_Raffle, List<decimal> Size, int NumberItem)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "INSERT INTO Bet(ID_of_Item, ID_of_Raffle, Size, Number_Items) VALUES (@ID_of_Item, @ID_of_Raffle, @Size, @Number_Items);";
                command.Parameters.Add("@ID_of_Item", System.Data.DbType.Int32).Value = ID_of_Item;
                command.Parameters.Add("@ID_of_Raffle", System.Data.DbType.Int32).Value = ID_of_Raffle;
                command.Parameters.Add("@Size", System.Data.DbType.Decimal).Value = Size;
                command.Parameters.Add("@Number_Items", System.Data.DbType.Int32).Value = NumberItem;
                command.ExecuteNonQuery();
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
            public static bool CheckNum(int Num, int ID_Raffle)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Bet WHERE Number_Items=@num AND ID_of_Raffle=@idRaffle";
                command.Parameters.Add("@num", System.Data.DbType.Int32).Value = Num;
                command.Parameters.Add("@idRaffle", System.Data.DbType.Int32).Value = ID_Raffle;
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
        public static class Items
        {
            public static void FindItems(string NameOwner, out int[] ID, out string[] Name, out string[] Way)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT * FROM Items WHERE Name_owner=@namegame";
                command.Parameters.Add("@namegame", System.Data.DbType.String).Value = NameOwner;
                reader = command.ExecuteReader();
                int length = 6;
                ID = new int[length];
                Name = new string[length];
                Way = new string[length];
                for(int i = 0; reader.Read() ; i++)
                {
                    ID[i] = Convert.ToInt32(reader["ID_Items"]);
                    Name[i] = Convert.ToString(reader["Name"]);
                    Way[i] = Convert.ToString(reader["Way"]);
                }
            }
            public static int GetCountInGame(string NameGame)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Items WHERE Name_owner=@namegame";
                command.Parameters.Add("@namegame", System.Data.DbType.String).Value = NameGame;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static Pair<int, string>[] GetNames(string GameName)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Name, ID_Items FROM Items WHERE Name_owner=@namegame";
                command.Parameters.Add("@namegame", System.Data.DbType.String).Value = GameName;
                reader = command.ExecuteReader();
                Pair<int, string>[] Items = new Pair<int, string>[GetCountInGame(GameName)];
                for (int i = 0; i < Items.Length; i++)
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
            public static int GetIdItem(string Name)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT ID_Items FROM Items WHERE Name=@name";
                command.Parameters.Add("@name", System.Data.DbType.String).Value = Name;
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public static class Persone
        {
            public static int CreatePersone(string Name, string Pass, decimal Money = 100)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                if(Money > 0)
                {
                    command.CommandText = "INSERT INTO Persone(Name, Pass, Money) VALUES (@Name, @Pass, @Money);" +
                                        "SELECT last_insert_rowid() AS ID_Persone FROM Persone LIMIT 1;";
                    command.Parameters.Add("@Money", System.Data.DbType.Decimal).Value = Money;
                }
                else
                {
                    command.CommandText = "INSERT INTO Persone(Name, Pass) VALUES (@Name, @Pass);" +
                                        "SELECT last_insert_rowid() AS ID_Persone FROM Persone LIMIT 1;";
                }
                command.Parameters.Add("@Name", System.Data.DbType.String).Value = Name;
                command.Parameters.Add("@Pass", System.Data.DbType.String).Value = Pass;
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
            public static decimal GetMoney(int ID_Persone)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Money FROM Persone WHERE ID_Persone=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Persone;
                return Convert.ToDecimal(command.ExecuteScalar());
            }
            public static string GetName(int ID_Persone)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Name FROM Persone WHERE ID_Persone=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Persone;
                return Convert.ToString(command.ExecuteScalar());
            }
        }
        public static class Raffle
        {
            public static int CreateRaffle(int ID_of_Persone, int WinNum, int NameGame)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "INSERT INTO Raffle(ID_of_Persone, Win_Number, ID_of_NameGames) VALUES (@ID_of_Persone, @Win_Number, @ID_of_NameGame);" +
                                        "SELECT last_insert_rowid() AS ID_Raffle FROM Raffle LIMIT 1;";
                command.Parameters.Add("@ID_of_Persone", System.Data.DbType.Int32).Value = ID_of_Persone;
                command.Parameters.Add("@Win_Number", System.Data.DbType.Int32).Value = WinNum;
                command.Parameters.Add("@ID_of_NameGame", System.Data.DbType.Int32).Value = NameGame;
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
            public static int GetCountRaffle()
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Raffle";
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static int GetCountRaffle(int ID_Persone)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Raffle WHERE ID_of_Persone=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID_Persone;
                return Convert.ToInt32(command.ExecuteScalar());
            }
            public static void GetStringResult(int count, out string[] PlayerName, out decimal[] Result, out string[] NameGame, int ID = -1)
            {
                PlayerName = new string[count];
                Result = new decimal[count];
                NameGame = new string[count];

                SQLiteCommand command = DBConnect.CreateCommand();

                if (ID > 0)
                    command.CommandText += "SELECT * FROM Raffle  WHERE ID_of_Persone=@id ORDER BY ID_Raffle DESC LIMIT @COUNT";
                else
                    command.CommandText = "SELECT * FROM Raffle ORDER BY ID_Raffle DESC LIMIT @COUNT";

                command.Parameters.Add("@COUNT", System.Data.DbType.Int32).Value = count;
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID;
                reader = command.ExecuteReader();
                
                for(int i = 0; (i < count) && (reader.Read()); i++)
                {
                    int ID_of_Persone = Convert.ToInt32(reader["ID_of_Persone"]);
                    Result[i] = Convert.ToDecimal(reader["Result"]);
                    int ID_of_NameGames = Convert.ToInt32(reader["ID_of_NameGames"]);
                    NameGame[i] = NameGames.GetName(ID_of_NameGames);

                    PlayerName[i] = (ID_of_Persone == -42) ? "Гость" : Persone.GetName(ID_of_Persone);
                }
            }

        }
        public static class NameGames
        {
            public static string GetName(int ID)
            {
                SQLiteCommand command = DBConnect.CreateCommand();
                command.CommandText = "SELECT Name FROM NameGames WHERE ID_NameGame=@id";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID;
                return Convert.ToString(command.ExecuteScalar());
            }
        }
    }
}