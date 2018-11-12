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
    public partial class Player : Window
    {
        private Logic.Player player;
        internal Player(Logic.Player player)
        {
            InitializeComponent();
            this.player = player;
            Login.Content = player.Name;
            balance.Content = Math.Round(player.Money, 1);

            ShowHistory();
        }
        private void authorization_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Authorization(player));
        }
        private void registration_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Registration(player));
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this,new Hall(player));
        }
        private void ShowHistory()
        {
            if(player is Logic.Guest)
            {
                TextBox textBox = new TextBox();
                textBox.IsEnabled = false;
                textBox.Height = 120;
                textBox.FontSize = 22;
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.HorizontalAlignment = HorizontalAlignment.Center;
                textBox.Text = "Для отображения прошлых игр требуется аутентификация";
                History.Children.Add(textBox);
                return;
            }
            int Count = Logic.Table.Raffle.GetCountRaffle((player as Logic.Persone).ID);
            Count = (Count > 20) ? 20 : Count;
            string[] GameName;
            string[] PersoneName;
            decimal[] Result;
            Logic.Table.Raffle.GetStringResult(Count, out PersoneName, out Result, out GameName, (player as Logic.Persone).ID);

            for (int i = 0; i < Count; i++)
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
