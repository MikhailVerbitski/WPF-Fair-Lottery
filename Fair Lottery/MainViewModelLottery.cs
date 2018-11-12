using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Collections.ObjectModel;

namespace Fair_Lottery
{
    partial class MainViewModel
    {
        private double lotterySlider;
        public double LotterySlider
        {
            get { return lotterySlider; }
            set
            {
                lotterySlider = value;
                OnPropertyChanged("LotterySlider");
            }
        }
        private double lotterySliderMaximum;
        public double LotterySliderMaximum
        {
            get { return lotterySliderMaximum; }
            set
            {
                lotterySliderMaximum = value;
                OnPropertyChanged("LotterySliderMaximum");
            }
        }

        private Visibility lotteryVisibilityHiddenElement = Visibility.Hidden;
        public Visibility LotteryVisibilityHiddenElement
        {
            get { return lotteryVisibilityHiddenElement; }
            set
            {
                lotteryVisibilityHiddenElement = value;
                OnPropertyChanged("LotteryVisibilityHiddenElement");
            }
        }

        private bool lottryisEnableElement = true;
        public bool LottryIsEnableElement
        {
            get { return lottryisEnableElement; }
            set
            {
                lottryisEnableElement = value;
                OnPropertyChanged("LottryIsEnableElement");
            }
        }

        private int lotteryWinNum;
        public int LotteryWinNum
        {
            get { return lotteryWinNum; }
            set
            {
                lotteryWinNum = value;
                OnPropertyChanged("LotteryWinNum");
            }
        }

        private decimal lotteryWinnings;
        public decimal LotteryWinnings
        {
            get { return lotteryWinnings; }
            set
            {
                lotteryWinnings = value;
                OnPropertyChanged("LotteryWinnings");
            }
        }

        private decimal lotteryCosts;
        public decimal LotteryCosts
        {
            get { return lotteryCosts; }
            set
            {
                lotteryCosts = value;
                OnPropertyChanged("LotteryCosts");
            }
        }

        private decimal lottryResult;
        public decimal LottryResult
        {
            get { return lottryResult; }
            set
            {
                lottryResult = value;
                OnPropertyChanged("LottryResult");
            }
        }

        private string[] lotteryNumbers = new string[5]{ "", "", "", "", "" };
        public string[] LotteryNumbers
        {
            get { return lotteryNumbers; }
            set
            {
                lotteryNumbers = value;
                OnPropertyChanged("LotteryNumbers");
            }
        }

        private Command lotteyRestart;
        public Command LotteryRestart
        {
            get
            {
                return lotteyRestart ?? (lotteyRestart = new Command(obj => { Game = new Logic.Dice(this); }));
            }
        }

        private Command lotteryRaffle;
        public Command LotteryRaffle
        {
            get
            {
                return lotteryRaffle ?? (lotteryRaffle = new Command( (Game as Logic.Lottery).Button_Raffle ));
            }
        }

        private Command lotteryBuyFew;
        public Command LotteryBuyFew
        {
            get
            {
                return lotteryBuyFew ?? (lotteryBuyFew = new Command((Game as Logic.Lottery).BuyFew));
            }
        }

        private Command lotteryBuyTicket;
        public Command LotteryBuyTicket
        {
            get
            {
                return lotteryBuyTicket ?? (lotteryBuyTicket = new Command((Game as Logic.Lottery).BuyTicket));
            }
        }

        private Command lotteryGenerateNumber;
        public Command LotteryGenerateNumber
        {
            get
            {
                return lotteryGenerateNumber ?? (lotteryGenerateNumber = new Command((Game as Logic.Lottery).Button_Generate));
            }
        }
        
    }
}
