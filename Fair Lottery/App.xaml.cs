using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fair_Lottery
{
    public partial class App : Application
    {
        public static void SwapWindows(Window Close, Window Open)
        {
            Open.Top = Close.Top;
            Open.Left = Close.Left;
            Open.Width = Close.ActualWidth;
            Open.Height = Close.ActualHeight;
            Open.Show();
            Close.Close();
        }
    }
}
