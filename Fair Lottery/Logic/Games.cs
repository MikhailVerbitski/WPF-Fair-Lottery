﻿using System;
using System.Linq;
using System.Windows;

namespace Fair_Lottery.Logic
{
    interface Game
    {
        string Name { get; }
    }
    class Dice : Game
    {
        public string Name { get { return "Dice"; } }

        private ViewModel.DiceViewModel mainViewModel;
        private static int ID_Name = 2;

        public Dice(ViewModel.GameViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel as ViewModel.DiceViewModel;
            this.mainViewModel.DiceSliderMaximum = Convert.ToDouble(mainViewModel.GetPlayer.Money);
        }
        public void MakeBet(object obj)
        {
            decimal result = 0;
            decimal Ratio = 3;
            int WinNum = new Random(DateTime.Now.Millisecond).Next(1, 6);
            int ID_Raffle = Table.Raffle.CreateRaffle(mainViewModel.GetPlayer.ID, WinNum, ID_Name);
            int[] ID = new int[6] { 1, 2, 3, 4, 5, 6 };
            string[] buttons = new string[6] { "pack://siteoforigin:,,,/Resource/Dice_One.png",
                                             "pack://siteoforigin:,,,/Resource/Dice_Two.png",
                                             "pack://siteoforigin:,,,/Resource/Dice_Three.png",
                                             "pack://siteoforigin:,,,/Resource/Dice_Four.png",
                                             "pack://siteoforigin:,,,/Resource/Dice_Five.png",
                                             "pack://siteoforigin:,,,/Resource/Dice_Six.png" };
            for (int i = 0; i < 6; i++)
                if (mainViewModel.Rates[i] > 0)
                {
                    Table.Bet.CreateBet(ID[i], ID_Raffle, mainViewModel.Rates[i], ID[i]);
                    result -= mainViewModel.Rates[i];
                    result += (WinNum == ID[i]) ? mainViewModel.Rates[i] * Ratio : 0;
                }

            if (mainViewModel.GetPlayer is Persone)
            {
                Table.Persone.UpdateMoney(mainViewModel.GetPlayer.ID, result);
                (mainViewModel.GetPlayer as Persone).Refresh();
            }
            else
            {
                (mainViewModel.GetPlayer as Guest).Money += result;
            }
            Table.Raffle.SetResult(ID_Raffle, result);

            mainViewModel.WinImageSource = buttons[WinNum - 1];
            mainViewModel.Result = result.ToString();
            mainViewModel.GetBalance = Math.Round(mainViewModel.GetPlayer.Money, 1);

            mainViewModel.DiceIsEnableElement = false;
            mainViewModel.DiceVisibilityHiddenElement = Visibility.Visible;
        }
        public void Buy(object obj)
        {
            bool[] Checkbuttons = mainViewModel.IsCheckedButtons;
            mainViewModel.DiceProbability = "0";
            for (int i = 0; i < 6; i++)
                if (Checkbuttons[i])
                {
                    mainViewModel.Rates[i] += Convert.ToDecimal(mainViewModel.DiceSlider);
                    mainViewModel.OnPropertyChanged("Rates");
                    mainViewModel.GetBalance -= Convert.ToDecimal(mainViewModel.DiceSlider);
                    mainViewModel.DiceProbability = (Convert.ToDouble(mainViewModel.DiceProbability) + (1.0 / 6)).ToString();
                }
            mainViewModel.DiceProbability = Math.Round(Convert.ToDouble(mainViewModel.DiceProbability) * 100, 1) + "%";
        }
    }
    class Lottery : Game
    {
        public string Name { get { return "Lottery"; } }

        private ViewModel.LotteryViewModel mainViewModel;
        private static int ID_Name = 1;
        private decimal price;
        private decimal Bet;

        System.Collections.ObjectModel.ObservableCollection<int> Tickets = new System.Collections.ObjectModel.ObservableCollection<int>();
        System.Collections.ObjectModel.ObservableCollection<int> Rest = new System.Collections.ObjectModel.ObservableCollection<int>();

        public Lottery(ViewModel.GameViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel as ViewModel.LotteryViewModel;

            for (int i = 0; i < 100000; i++)
                Rest.Add(i);
            
            price = 1;
            Bet = 0;
            this.mainViewModel.LotterySliderMaximum = Convert.ToDouble(mainViewModel.GetPlayer.Money / price);
        }
        public void BuyFew(object obj)
        {
            for (int i = 0, n; i < mainViewModel.LotterySlider; i++)
            {
                n = GeneratorFreeTickets(i);
                Tickets.Add(Rest[n]);
                Rest.RemoveAt(n);
            }
            Bet += price * Convert.ToDecimal(mainViewModel.LotterySlider);
            mainViewModel.GetBalance = mainViewModel.GetPlayer.Money - Bet;
            mainViewModel.LotterySliderMaximum = Convert.ToDouble(mainViewModel.GetPlayer.Money - Bet);
            mainViewModel.PurchasedTickets = Tickets;
        }
        public void Button_Raffle(object obj)
        {
            int WinNum = new Random(DateTime.Now.Millisecond).Next(0, 99999);
            int ID_Raffle = Table.Raffle.CreateRaffle(mainViewModel.GetPlayer.ID, WinNum, ID_Name);

            //foreach (int num in Tickets)
            //    Table.Bet.CreateBet(7, ID_Raffle, price, num);
            Table.Bet.CreateBet(7, ID_Raffle, price, (Tickets.Where(n => n == WinNum).Count() > 0) ? Tickets.Where(n => n == WinNum).First() : Tickets.First());
            //  Заменено из-за большого потребления памяти

            decimal Winnings = (Tickets.Where(n => n == WinNum).Count() > 0) ? 50000m : 0;
            decimal Result = Winnings - Bet;
            mainViewModel.GetPlayer.Money += Result;
            Table.Persone.UpdateMoney(mainViewModel.GetPlayer.ID, Result);
            Table.Raffle.SetResult(ID_Raffle, Result);
            mainViewModel.GetBalance = mainViewModel.GetPlayer.Money;

            mainViewModel.LotteryWinNum = WinNum;
            mainViewModel.LotteryWinnings = Result + Bet;
            mainViewModel.LotteryCosts = Bet;
            mainViewModel.LottryResult = Result;

            mainViewModel.LottryIsEnableElement = false;

            mainViewModel.LotteryVisibilityHiddenElement = Visibility.Visible;
        }
        public void BuyTicket(object obj)
        {
            string str = string.Join(null, mainViewModel.LotteryNumbers);
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
                Tickets.Add(Num);
                Rest.Remove(Num);
                Bet += price;
                mainViewModel.GetBalance = mainViewModel.GetPlayer.Money - Bet;
                mainViewModel.LotterySliderMaximum = Convert.ToDouble(mainViewModel.GetPlayer.Money - Bet);
            }
            mainViewModel.PurchasedTickets = Tickets;
            mainViewModel.LotteryNumbers = new string[] { "", "", "", "", "",};
        }
        public void Button_Generate(object obj)
        {
            char[] mas = GeneratorFreeTickets().ToString().ToCharArray();
            for (int i = 0; i < 5; i++)
                mainViewModel.LotteryNumbers[i] = (mas.Count() > i) ? mas[i].ToString() : "0";
            mainViewModel.OnPropertyChanged("LotteryNumbers");
        }
        private int GeneratorFreeTickets(int step = 0)
        {
            return new Random(DateTime.Now.Millisecond + step).Next(0, Rest.Count() - 1);
        }
    }
    struct GameInfo
    {
        private string NameGame;
        private string NamePlayer;
        private decimal Result;
        public GameInfo(string NameGame, string NamePlayer, decimal Result)
        {
            this.NameGame = NameGame;
            this.NamePlayer = NamePlayer;
            this.Result = Result;
        }
        public override string ToString()
        {
            return NameGame + ": " + NamePlayer + " -> " + Result;
        }
    }
}