using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace Fair_Lottery.Logic
{
    class Dice : GameWindow.Dice
    {
        private Player persone;
        private static int ID_Name = 2;
        private double Probability = 0;
        private decimal[] Rates = new decimal[6] { 0, 0, 0, 0, 0, 0 };

        public Dice(Player persone)
        {
            InitializeComponent();
            this.persone = persone;
            
            balance.Content = Math.Round(persone.Money, 1);
            Slider.Maximum = Convert.ToDouble(persone.Money);
        }
        public override void MakeBet(object sender, RoutedEventArgs e)
        {
            decimal result = 0;
            decimal Ratio = 3;
            int WinNum = new Random(DateTime.Now.Millisecond).Next(1, 6);
            int ID_Raffle = Table.Raffle.CreateRaffle(persone.ID, WinNum, ID_Name);
            int[] ID = new int[6] { 1, 2, 3, 4, 5, 6 };
            System.Windows.Controls.Primitives.ToggleButton[] buttons = new System.Windows.Controls.Primitives.ToggleButton[6] { One, Two, Three, Four, Five, Six };

            for (int i = 0; i < 6; i++)
                if (Rates[i] > 0)
                {
                    Table.Bet.CreateBet(ID[i], ID_Raffle, Rates[i], ID[i]);
                    result -= Rates[i];
                    result += (WinNum == ID[i]) ? Rates[i] * Ratio : 0;
                }

            if (persone is Persone)
            {
                Table.Persone.UpdateMoney(persone.ID, result);
                (persone as Persone).Refresh();
            }
            else
            {
                (persone as Guest).Money += result;
            }
            Table.Raffle.SetResult(ID_Raffle, result);
            
            WinImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(buttons[WinNum - 1].Tag.ToString()));
            Start.IsEnabled = false;
            (sender as System.Windows.Controls.Button).IsEnabled = false;
            Result.Content = result;
            balance.Content = Math.Round(persone.Money, 1);

            WinLabImag.Visibility = Visibility.Visible;
            TextResult.Visibility = Visibility.Visible;
            Result.Visibility = Visibility.Visible;
            Restart.Visibility = Visibility.Visible;
        }
        public override void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Money.Content = e.NewValue;
        }
        public override void Back(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Windows.Hall(persone));
        }
        public override void Restart_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Dice(persone));
        }
        public override void Buy(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Primitives.ToggleButton[] buttons = new System.Windows.Controls.Primitives.ToggleButton[6] { One, Two, Three, Four, Five, Six };
            System.Windows.Controls.TextBlock[] textBlocks = new System.Windows.Controls.TextBlock[6] { RateOne, RateTwo, RateThree, RateFour, RateFive, RateSix };
            Probability = 0;
            for (int i = 0; i < 6; i++)
                if ((bool)buttons[i].IsChecked)
                {
                    Rates[i] += Convert.ToDecimal(Slider.Value);
                    textBlocks[i].Text = (Convert.ToDecimal(textBlocks[i].Text) + Convert.ToDecimal(Slider.Value)).ToString();
                    balance.Content = Convert.ToDouble(balance.Content) - Slider.Value;
                    Probability += (1.0 / 6);
                }
            probability.Content = Math.Round(Probability * 100, 1) + "%";
        }
    }
    class Lottery : GameWindow.Lottery
    {
        private static int ID_Name = 1;
        private List<int> Tickets = new List<int>();
        private List<int> Rest = new List<int>();
        private Player persone;
        private decimal price;
        private decimal Bet;
        
        public Lottery(Player persone)
        {
            InitializeComponent();
            this.persone = persone;

            for (int i = 0; i < 100000; i++)
                Rest.Add(i);

            balance.Content = persone.Money;
            price = 1;
            Bet = 0;
            SliderBuyFew.Maximum = Convert.ToDouble(persone.Money / price);
        }
        public override void Button_Generate(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox[] textBoxs = new System.Windows.Controls.TextBox[5] { InFirst, InSecond, InThird, InFourth, InFifth };
            
            char[]mas = Button_Generate().ToString().ToCharArray();

            for (int i = 0; i < 5; i++)
                textBoxs[i].Text = (mas.Count() > i) ? mas[i].ToString() : "0";
        }
        public override void BuyTicket(object sender, RoutedEventArgs e)
        {
            string str = InFirst.Text + InSecond.Text + InThird.Text + InFourth.Text + InFifth.Text;
            if (str == "") str = "00000";
            int Num = Convert.ToInt32(str);
            if (Tickets.Where(n => n == Num).Count() > 0)
            {
                System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                label.Height = 30;
                label.FontSize = 14;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Content = "Уже куплен";
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }
            else
            {

            }
            ShowLast();
            InFirst.Text = InSecond.Text = InThird.Text = InFourth.Text = InFifth.Text = "";
        }

        private void ShowLast()
        {
            int Count = (Tickets.Count() < 20) ? Tickets.Count() : 20;
            for (int i = 0; i < Count; i++)
            {
                System.Windows.Controls.Label label = new System.Windows.Controls.Label();
                label.Height = 30;
                label.FontSize = 14;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.Content = Tickets[Tickets.Count - i - 1];
                StackTickets.Children.Add(label);
            }
        }
        public override void Button_Raffle(object sender, RoutedEventArgs e)
        {
            int WinNum = new Random(DateTime.Now.Millisecond).Next(0, 99999);
            int ID_Raffle = Table.Raffle.CreateRaffle(persone.ID, WinNum, ID_Name);

            //foreach (int num in Tickets)
            //    Table.Bet.CreateBet(7, ID_Raffle, price, num);
            Table.Bet.CreateBet(7, ID_Raffle, price, (Tickets.Where(n => n == WinNum).Count() > 0) ? Tickets.Where(n => n == WinNum).First() : Tickets.First());
            //  Заменено из-за большого потребления памяти

            persone.Money -= Bet;
            Table.Persone.UpdateMoney(persone.ID, -Bet);
            decimal Result = (Tickets.Where(n => n == WinNum).Count() > 0) ? 10000m : -Bet;
            Table.Raffle.SetResult(ID_Raffle, Result);
            
            winNum.Content = WinNum.ToString();
            Winnings.Text = (Result + Bet).ToString();
            Buys.Text = Bet.ToString();
            result.Text = Result.ToString();

            buyFew.IsEnabled = false;
            Generate.IsEnabled = false;
            Buy.IsEnabled = false;
            raffle.IsEnabled = false;

            TextWin.Visibility = Visibility.Visible;
            winNum.Visibility = Visibility.Visible;
            PlayAgain.Visibility = Visibility.Visible;
            blabla.Visibility = Visibility.Visible;
        }
        public override void Restart_Click(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Lottery(persone));
        }
        public override void Back(object sender, RoutedEventArgs e)
        {
            App.SwapWindows(this, new Windows.Hall(persone));
        }
        public override void Slider_ValueChanged(object sender, RoutedEventArgs e)
        {
            SliderCount.Content = SliderBuyFew.Value;
        }
        public override void BuyFew(object sender, RoutedEventArgs e)
        {

        }
    }
}