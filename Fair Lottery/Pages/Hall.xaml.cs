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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fair_Lottery.Pages
{
    public partial class Hall : Page
    {
        internal Hall(ViewModel.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void StartLottery(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.MainViewModel).AddGame(ViewModel.GamesEnum.Lottery);
        }

        private void StartDice(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.MainViewModel).AddGame(ViewModel.GamesEnum.Dice);
        }
    }
}
