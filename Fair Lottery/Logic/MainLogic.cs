using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Fair_Lottery.Windows;

namespace Logic
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
    static class MainLogic
    {
        public static Persone persone;
        public static decimal StartMoney { get { return 100; } }

        public static Authentication Authentication { get; private set; }
        public static Authorization Authorization { get; private set; }
        public static Hall Hall { get; private set; }
        public static Registration Registration { get; private set; }

        static Game game;

        public static void InitializationLogic()
        {
            Table.ConnectWithDataBase();

            Authentication = new Authentication();
            Authorization = new Authorization();
            Registration = new Registration();
        }
        public static void CloseOpen(Window Close, Window Open)
        {
            Open.Height = Close.ActualHeight;
            Open.Width = Close.ActualWidth;
            Open.Top = Close.Top;
            Open.Left = Close.Left;
            Open.Show();
            Close.Close();
        }
    }
}
