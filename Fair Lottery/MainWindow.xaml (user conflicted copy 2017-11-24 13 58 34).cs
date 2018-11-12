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

namespace Fair_Lottery
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void SkipBot(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation animation = new System.Windows.Media.Animation.DoubleAnimation();
            animation.From = Bot.ActualHeight;
            animation.To = (Bot.ActualHeight == 0) ? 60 : 0;
            animation.Duration = TimeSpan.FromSeconds(1);

            Bot.BeginAnimation(HeightProperty, animation);
        }
    }
}
