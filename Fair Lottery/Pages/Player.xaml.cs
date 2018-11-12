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
    public partial class Player : Page
    {
        internal Player(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void AuthorizationClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).ActuallyBody = new Authorization(DataContext as MainViewModel);
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).ActuallyBody = new Registration(DataContext as MainViewModel);
        }
    }
}
