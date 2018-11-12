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

namespace Fair_Lottery
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Page actuallyTop;
        private Page actuallyBottom;
        private Page actuallyBody;
        private Page actuallyRightPanel;

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
        public Page ActuallyRightPanel
        {
            get { return actuallyRightPanel; }
            set
            {
                actuallyRightPanel = value;
                OnPropertyChanged("ActuallyRightPanel");
            }
        }

        public MainViewModel()
        {
            actuallyTop = new Pages.TopPanel(this);
            actuallyBottom = new Pages.BottomPanel(this);
            actuallyBody = new Pages.Hall(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
