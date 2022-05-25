using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using Extender.Abstractions;
using Extender.Implementations;
using Xamarin.Forms;
using System.Linq;
using Extender.Commands;
using System.Collections.ObjectModel;
namespace Extender.ViewModels
{
    public class MailViewModelList : INotifyPropertyChanged
    {
        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        private ObservableCollection<MailViewModel> _mails;
        public ObservableCollection<MailViewModel> Mails { get { return _mails; } set { _mails = value;OnPropertyChanged(); } }
        public MailViewModelList()
        {
            Mails = new ObservableCollection<MailViewModel>();
            AddCommand = new MailAddCommand(this);
            RemoveCommand = new Command((arg) =>
             {
                 _mails.Remove(arg as MailViewModel);
             });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
