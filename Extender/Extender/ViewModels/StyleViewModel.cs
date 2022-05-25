using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
namespace Extender.ViewModels
{
    public class StyleViewModel:INotifyPropertyChanged
    {
        private Style _style;
        public event PropertyChangedEventHandler PropertyChanged;
        public Style Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
                OnPropertyChanged();
            }
        }
        public StyleViewModel(Style style)
        {
            _style = style;
        }
        public void OnPropertyChanged([CallerMemberName]string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
