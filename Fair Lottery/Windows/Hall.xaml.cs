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
    public partial class Hall : Window
    {
        private Logic.Player player;
        internal Hall(Logic.Player player)
        {
            InitializeComponent();
            this.player = player;
            Login.Content = player.Name;
            Money.Content = player.Money;

            ShowHistory();
        }
        private void ButtonDice_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Logic.Dice(player));
        }
        private void ButtonLottery_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new  Logic.Lottery(player));
        }
        private void Player_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Player(player));
        }
        private void ShowHistory()
        {
            int Count = Logic.Table.Raffle.GetCountRaffle();
            Count = (Count > 20) ? 20 : Count;
            string[] GameName;
            string[] PersoneName;
            decimal[] Result;
            Logic.Table.Raffle.GetStringResult(Count,out PersoneName,out Result,out GameName);

            for (int i = 0; i< Count; i++)
            {
                Label label = new Label();
                label.Height = 23;
                
                label.Content = GameName[i] + " | " + PersoneName[i] + " | " + Result[i].ToString();
                label.Background = (Result[i] <= 0) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
                History.Children.Add(label);
            }
        }
    }
}
