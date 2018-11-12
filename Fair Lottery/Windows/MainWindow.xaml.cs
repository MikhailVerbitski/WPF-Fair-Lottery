using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fair_Lottery.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ClickMe(object sender, EventArgs e)
        {
            Logic.Table.ConnectWithDataBase();
            App.SwapWindows(this, new Hall(Logic.Player.GetPlayer()));
        }
    }
}
