using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Fair_Lottery.ViewModel
{
    class DiceViewModel : GameViewModel, INotifyPropertyChanged
    {
        public DiceViewModel(Game game) : base(game) { }

        private string diceProbability = "0";
        public string DiceProbability
        {
            get { return diceProbability; }
            set
            {
                diceProbability = value;
                OnPropertyChanged("DiceProbability");
            }
        }

        private ObservableCollection<int> purchasedTickets;
        public ObservableCollection<int> PurchasedTickets
        {
            get { return purchasedTickets; }
            set
            {
                purchasedTickets = value;
                OnPropertyChanged("PurchasedTickets");
            }
        }

        private double diceSlider;
        public double DiceSlider
        {
            get { return diceSlider; }
            set
            {
                diceSlider = value;
                OnPropertyChanged("DiceSlider");
            }
        }

        private double diceSliderMaximum;
        public double DiceSliderMaximum
        {
            get { return diceSliderMaximum; }
            set
            {
                diceSliderMaximum = value;
                OnPropertyChanged("DiceSliderMaximum");
            }
        }

        private Visibility diceVisibilityHiddenElement = Visibility.Hidden;
        public Visibility DiceVisibilityHiddenElement
        {
            get { return diceVisibilityHiddenElement; }
            set
            {
                diceVisibilityHiddenElement = value;
                OnPropertyChanged("DiceVisibilityHiddenElement");
            }
        }

        private string winImageSource;
        public string WinImageSource
        {
            get { return winImageSource; }
            set
            {
                winImageSource = value;
                OnPropertyChanged("WinImageSource");
            }
        }

        private bool diceisEnableElement = true;
        public bool DiceIsEnableElement
        {
            get { return diceisEnableElement; }
            set
            {
                diceisEnableElement = value;
                OnPropertyChanged("DiceIsEnableElement");
            }
        }

        private string result;
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        private decimal[] rates = { 0, 0, 0, 0, 0, 0 };
        public decimal[] Rates
        {
            get { return rates; }
            set
            {
                rates = value;
            }
        }
        private bool[] isCheckedButtons = new bool[6] { false, false, false, false, false, false };
        public bool[] IsCheckedButtons
        {
            get { return isCheckedButtons; }
            set
            {
                isCheckedButtons = value;
            }
        }

        private Command diceBuy;
        public Command DiceBuy { get { return diceBuy ?? (diceBuy = new Command( (game.LogicGame as Logic.Dice).Buy) ); } }
        private Command diceMakeBet;
        public Command DiceMakeBet { get { return diceMakeBet ?? (diceMakeBet = new Command((game.LogicGame as Logic.Dice).MakeBet)); } }
    }
}
