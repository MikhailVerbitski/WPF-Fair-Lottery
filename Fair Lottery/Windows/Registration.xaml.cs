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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Logic.Table.Persone.CheckPersone(Login.ToString()))
            {
                HiddenBlock.Visibility = Visibility.Visible;
            }
            else
            {
                Logic.MainLogic.persone = new Logic.Persone(Logic.Table.Persone.CreatePersone(Login.ToString(), Pass.ToString(), Logic.MainLogic.StartMoney), 
                                                            Login.ToString(), 
                                                            Pass.ToString(), 
                                                            Logic.MainLogic.StartMoney);
                Logic.MainLogic.CloseOpen(this, Logic.MainLogic.Hall);
            }
        }
    }
}
