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

using Microsoft.Win32;

namespace Fair_Lottery.ViewModel
{
    abstract class GameViewModel
    {
        protected Game game;
        public Logic.Player GetPlayer { get { return game.mainViewModel.VMPlayer; } set { game.mainViewModel.VMPlayer = value; } }
        public decimal GetBalance { get { return game.mainViewModel.Balance; } set { game.mainViewModel.Balance = value; } }

        public GameViewModel(Game game)
        {
            this.game = game;
        }
    }
    class MainViewModel : INotifyPropertyChanged
    {
        private Logic.Player player = new Logic.Guest();
        private List<Game> games = new List<Game>();

        private Page actuallyTop;
        private Page actuallyBottom;
        private Page actuallyBody;
        private decimal balance;

        private ObservableCollection<Logic.GameInfo> lastGames;

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
        public decimal Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
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

        public void AddGame(GamesEnum game)
        {
            games.Add(new Game(game, this));
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
            Home.Command = new Command((obj) => { ActuallyBody = new Pages.Hall(this); });
            Home.Content = "Home";
            Button Player = new Button();
            Player.Command = new Command((obj) => { ActuallyBody = new Pages.Player(this); });
            Player.Content = "Player";

            bottomPanelButtons.Add(Home);
            bottomPanelButtons.Add(Player);

            VisibilityTextRegistration = Visibility.Hidden;
        }
        public void RefreshLastGame() => LastGames = Logic.Table.Raffle.GetLastGameInfo();

        ////Player  //////////////////////////////////////////////////////////////////////////////////
        public Command Authorization { get { return new Command( obj => ActuallyBody = new Pages.Authorization(this) ); } }
        public void AuthorizationClick(string Login, string Pass)
        {
            VMPlayer = Logic.Persone.GetPlayer(true, Login, Pass);
            ActuallyBody = new Pages.Player(this);
        }
        public Visibility VisibilityTextRegistration { get; set; }
        public void RegistrationClick(string Login, string Pass)
        {
            if (Logic.Table.Persone.CheckPersone(Login))
            {
                VisibilityTextRegistration = Visibility.Visible;
            }
            else
            {
                VMPlayer = new Logic.Persone(Logic.Table.Persone.CreatePersone(Login, Pass), Login, Pass);
                ActuallyBody = new Pages.Player(this);
            }
        }
        public Command Registration { get { return new Command(obj => ActuallyBody = new Pages.Registration(this) ); } }
        public Command Serialization
        {
            get
            {
                return new Command(obj => {
                    if (player is Logic.Guest)
                    {
                        MessageBox.Show("Нельзя сохранить гостя!");
                        return;
                    }
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Fair_Lottery_Player |*.FLPlayer";
                    saveFileDialog.Title = "Сохранить пользователя";
                    saveFileDialog.ShowDialog();
                    (player as Logic.Persone).Serializ(saveFileDialog.FileName);
                });
            }
        }
        public Command Deserialization
        {
            get
            {
                return new Command(obj => {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Fair_Lottery_Player |*.FLPlayer";
                    openFileDialog.ShowDialog();
                    if(openFileDialog.FileName != "")
                        VMPlayer = Logic.Persone.Deserializ(openFileDialog.FileName);
                });
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////

        private ObservableCollection<Button> bottomPanelButtons = new ObservableCollection<Button>();
        public ObservableCollection<Button> BottomPanelButtons
        {
            get { return bottomPanelButtons; }
            set
            {
                bottomPanelButtons = value;
                OnPropertyChanged("BottomPanelButtons");
            }
        }
        public void AddButtons(Button button)
        {
            bottomPanelButtons.Add(button);
            OnPropertyChanged("BottomPanelButtons");
        }

        ///////////////////////////////////////////////////////////////////////////////
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    class Game
    {
        public Logic.Game LogicGame;
        public GameViewModel GameViewModel;
        public MainViewModel mainViewModel;
        public Page page;
        public Button GameButton = new Button();

        public Game(GamesEnum games, MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            if(games == GamesEnum.Dice)
            {
                GameViewModel = new DiceViewModel(this);
                LogicGame = new Logic.Dice(GameViewModel);
                page = new Pages.Games.Dice(GameViewModel as DiceViewModel);
            }
            else if(games == GamesEnum.Lottery)
            {
                GameViewModel = new LotteryViewModel(this);
                LogicGame = new Logic.Lottery(GameViewModel);
                page = new Pages.Games.Lottery(GameViewModel as LotteryViewModel);
            }

            GameButton.Content = LogicGame.Name;
            GameButton.Command = new Command((obj) => { mainViewModel.ActuallyBody = page; });
            mainViewModel.ActuallyBody = page;
            mainViewModel.AddButtons(GameButton);
        }
    }

    enum GamesEnum
    {
        Dice,
        Lottery
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