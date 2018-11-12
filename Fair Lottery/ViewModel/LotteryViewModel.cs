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

namespace Fair_Lottery.ViewModel
{
    class LotteryViewModel : GameViewModel, INotifyPropertyChanged
    {
        public LotteryViewModel(Game game) : base(game) { }

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

        private string[] lotteryNumbers = new string[5] { "", "", "", "", "" };
        public string[] LotteryNumbers
        {
            get { return lotteryNumbers; }
            set
            {
                lotteryNumbers = value;
                OnPropertyChanged("LotteryNumbers");
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<int> purchasedTickets;
        public System.Collections.ObjectModel.ObservableCollection<int> PurchasedTickets
        {
            get { return purchasedTickets; }
            set
            {
                purchasedTickets = value;
                OnPropertyChanged("PurchasedTickets");
            }
        }

        private Command lotteyRestart;
        public Command LotteryRestart
        {
            get
            {
                return lotteyRestart ?? (lotteyRestart = new Command(obj => {
                    game.GameViewModel = new LotteryViewModel(game);
                    game.LogicGame = new Logic.Lottery(game.GameViewModel);
                    game.page = new Pages.Games.Lottery(game.GameViewModel as ViewModel.LotteryViewModel);
                    game.mainViewModel.ActuallyBody = game.page;
                }));
            }
        }

        private Command lotteryRaffle;
        public Command LotteryRaffle
        {
            get
            {
                return lotteryRaffle ?? (lotteryRaffle = new Command((game.LogicGame as Logic.Lottery).Button_Raffle));
            }
        }

        private Command lotteryBuyFew;
        public Command LotteryBuyFew
        {
            get
            {
                return lotteryBuyFew ?? (lotteryBuyFew = new Command((game.LogicGame as Logic.Lottery).BuyFew));
            }
        }

        private Command lotteryBuyTicket;
        public Command LotteryBuyTicket
        {
            get
            {
                return lotteryBuyTicket ?? (lotteryBuyTicket = new Command((game.LogicGame as Logic.Lottery).BuyTicket));
            }
        }

        private Command lotteryGenerateNumber;
        public Command LotteryGenerateNumber
        {
            get
            {
                return lotteryGenerateNumber ?? (lotteryGenerateNumber = new Command((game.LogicGame as Logic.Lottery).Button_Generate));
            }
        }

        public Command GameDie
        {
            get
            {
                return new Command((obj) => {
                    game.mainViewModel.BottomPanelButtons.Remove(game.GameButton);
                    game.mainViewModel.OnPropertyChanged("BottomPanelButtons");
                    game.mainViewModel.ActuallyBody = new Pages.Hall(game.mainViewModel);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
