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
    /// Логика взаимодействия для Hall.xaml
    /// </summary>
    public partial class Hall : Window
    {
        public Hall()
        {
            InitializeComponent();
            Login.Content = Logic.MainLogic.persone.GetName;
        }
    }
}
