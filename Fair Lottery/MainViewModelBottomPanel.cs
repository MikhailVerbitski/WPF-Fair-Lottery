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

using System.Collections.ObjectModel;

namespace Fair_Lottery
{
    partial class MainViewModel
    {
        private List<Button> bottomPanelButtons = new List<Button>();
        public IEnumerable<Button> BottomPanelButtons
        {
            get
            {
                return bottomPanelButtons.Where( obj => obj is Button);
            }
            set
            {
                foreach(Button i in value.ToList<Button>())
                    bottomPanelButtons.Add(i);

            }
        }
    }
}
