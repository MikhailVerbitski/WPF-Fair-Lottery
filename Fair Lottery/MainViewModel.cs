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
    partial class MainViewModel : INotifyPropertyChanged
    {
        private Logic.Player player = new Logic.Guest();
        private Logic.Game game;

        private Page actuallyTop;
        private Page actuallyBottom;
        private Page actuallyBody;
        private decimal balance;

        private ObservableCollection<Logic.GameInfo> lastGames;
        private ObservableCollection<Button> bottomPanelListButton;

        private Button GameButton = new Button();

        public Logic.Player VMPlayer
        {
            get { return player; }
            set
            {
                player = value;
                balance = player.Money;
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
        public ObservableCollection<Button> BottomPanelListButton
        {
            get { return bottomPanelListButton; }
            set
            {
                bottomPanelListButton = value;
                OnPropertyChanged("BottomPanelListButton");
            }
        }
        public Logic.Game Game
        {
            get { return game; }
            set
            {
                game = value;
                Page page;
                if (game is Logic.Dice)
                {
                    page = new Pages.Games.Dice(this);
                    ActuallyBody = page;
                    GameButton.Command = new Command( (obj) => { ActuallyBody = page; } );
                    GameButton.Content = "Dice";
                }
                else if (game is Logic.Lottery)
                {
                    page = new Pages.Games.Lottery(this);
                    ActuallyBody = page;
                    GameButton.Command = new Command((obj) => { ActuallyBody = page; });
                    GameButton.Content = "Lottery";
                }
                bottomPanelButtons.Remove(GameButton);
                bottomPanelButtons.Add(GameButton);
                OnPropertyChanged("BottomPanelButtons");
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


            Button Home = new Button();
            Home.Command = new Command( (obj) => { ActuallyBody = new Pages.Hall(this); });
            Home.Content = "Home";
            Button Player = new Button();
            Player.Command = new Command((obj) => { ActuallyBody = new Pages.Player(this); });
            Player.Content = "Player";

            bottomPanelButtons.Add(Home);
            bottomPanelButtons.Add(Player);
        }

        public Command GameDie
        {
            get
            {
                return new Command((obj) => {
                    bottomPanelButtons.Remove(GameButton);
                    OnPropertyChanged("BottomPanelButtons");
                    ActuallyBody = new Pages.Hall(this);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class Command : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
