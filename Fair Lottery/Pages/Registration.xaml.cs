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
    public partial class Registration : Page
    {
        internal Registration(ViewModel.MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Logic.Table.Persone.CheckPersone(Login.Text))
            {
                HiddenBlock.Visibility = Visibility.Visible;
            }
            else
            {
                (DataContext as ViewModel.MainViewModel).VMPlayer = new Logic.Persone(Logic.Table.Persone.CreatePersone(Login.Text, Pass.Password), Login.Text, Pass.Password);
                (DataContext as ViewModel.MainViewModel).ActuallyBody = new Player(DataContext as ViewModel.MainViewModel);
            }
        }
    }
}
