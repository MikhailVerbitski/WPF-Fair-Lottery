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
    public partial class Authorization : Window
    {
        private Logic.Player player;
        internal Authorization(Logic.Player player)
        {
            InitializeComponent();
            this.player = player;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Hall(Logic.Persone.GetPlayer(true, Login.Text, Pass.Password)));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Player(player));
        }
    }
}
