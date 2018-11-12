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
using System.Windows.Shapes;

namespace Fair_Lottery.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {
            InitializeComponent();
        }
        private void authorization_Click(object sender, RoutedEventArgs e)
        {
            Logic.MainLogic.CloseOpen(this, Logic.MainLogic.Authorization);
        }
        private void registration_Click(object sender, RoutedEventArgs e)
        {
            Logic.MainLogic.CloseOpen(this, Logic.MainLogic.Registration);
        }
    }
}
