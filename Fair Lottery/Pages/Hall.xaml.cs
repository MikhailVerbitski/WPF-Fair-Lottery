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
        internal Hall(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            (DataContext as MainViewModel).ActuallyRightPanel = new HallRightPanel(mainViewModel);
        }

        private void ButtonLottery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
