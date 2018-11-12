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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int ID = Logic.Table.Persone.FindPersone(Login.Text, Pass.Password);
            
            Logic.MainLogic.persone = new Logic.Persone(ID, Login.Text, Pass.Password, Logic.Table.Persone.GetMoney(ID));
            Logic.MainLogic.CloseOpen(this, Logic.MainLogic.Hall);
        }
    }
}
