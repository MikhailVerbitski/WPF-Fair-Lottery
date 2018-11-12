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
    class MainViewModel : INotifyPropertyChanged
    {
        private Logic.Player player = new Logic.Guest();

        private Page actuallyTop;
        private Page actuallyBottom;
        private Page actuallyBody;
        private decimal balance;

        private ObservableCollection<Logic.GameInfo> lastGames;

        public Logic.Player VMPlayer
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
                OnPropertyChanged("Balance");
                OnPropertyChanged("VMPlayer");
            }
        }
        public Page ActuallyTop
        {
            get { return actuallyTop; }
            set
            {
                actuallyTop = value;
                OnPropertyChanged("ActuallyTop");
            }
        }
        public Page ActuallyBottom
        {
            get { return actuallyBottom; }
            set
            {
                actuallyBottom = value;
                OnPropertyChanged("ActuallyBottom");
            }
        }
        public Page ActuallyBody
        {
            get { return actuallyBody; }
            set
            {
                actuallyBody = value;
                OnPropertyChanged("ActuallyBody");
            }
        }
        public ObservableCollection<Logic.GameInfo> LastGamePlayer
        {
            get
            {
                return (player.ID == -42) ? new ObservableCollection<Logic.GameInfo>() : Logic.Table.Raffle.GetLastGameInfo(player.ID);
            }
        }
        public ObservableCollection<Logic.GameInfo> LastGames
        {
            get { return lastGames; }
            set
            {
                lastGames = value;
                OnPropertyChanged("LastGames");
            }
        }
        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public MainViewModel()
        {
            Logic.Table.ConnectWithDataBase();

            actuallyTop = new Pages.TopPanel(this);
            actuallyBottom = new Pages.BottomPanel(this);
            actuallyBody = new Pages.Hall(this);

            lastGames = Logic.Table.Raffle.GetLastGameInfo();
            Balance = player.Money;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
