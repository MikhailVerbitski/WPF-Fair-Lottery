using System;
using System.Data.SQLite;
using System.Collections.ObjectModel;

namespace Fair_Lottery.Logic
{
    static class Table
    {
        static SQLiteConnection DBConnect = new SQLiteConnection(@"Data Source=..\..\..\DataBase\DataBase.db");
        static SQLiteDataReader reader;
        public static void ConnectWithDataBase()
        {
            DBConnect.Open();
        }
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
        public static ObservableCollection<GameInfo> GetLastGameInfo(int ID = -1)
        {
            SQLiteCommand command = DBConnect.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Raffle";
            int Length = Convert.ToInt32(command.ExecuteScalar());

            command = DBConnect.CreateCommand();
            ObservableCollection<GameInfo> gameInfo = new ObservableCollection<GameInfo>();

            if (ID > 0)
            {
                command.CommandText = "SELECT * FROM Raffle WHERE ID_of_Persone=@id ORDER BY ID_Raffle DESC";
                command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID;
            }
            else
                command.CommandText = "SELECT * FROM Raffle ORDER BY ID_Raffle DESC";
            reader = command.ExecuteReader();

            for (int i = 0; (i < Length) && (reader.Read()); i++)
            {
                int ID_of_Persone = Convert.ToInt32(reader["ID_of_Persone"]);
                int ID_of_NameGames = Convert.ToInt32(reader["ID_of_NameGames"]);

                gameInfo.Add(new GameInfo((ID_of_Persone == -42) ? "Гость" : GetName(ID_of_Persone), GetName(ID_of_NameGames), Convert.ToDecimal(reader["Result"])));
            }
            return gameInfo;
        }
        public static string GetName(int ID)
        {
            SQLiteCommand command = DBConnect.CreateCommand();
            command.CommandText = "SELECT Name FROM NameGames WHERE ID_NameGame=@id";
            command.Parameters.Add("@id", System.Data.DbType.Int32).Value = ID;
            return Convert.ToString(command.ExecuteScalar());
        }
    }
}