using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace Extender.ViewModels
{
    public class DownloadViewModelList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public DownloadViewModelList()
        {
            _items = new ObservableCollection<DownloadViewModel>();
            SearchCommand = new SearchCommand(this);
            RemoveCommand = new RemoveCommand(this);
        }
        private ObservableCollection<DownloadViewModel> _items;
        public ObservableCollection<DownloadViewModel> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
    }
    public class SearchCommand : ICommand
    {
        private DownloadViewModelList _src;
        public event EventHandler CanExecuteChanged;
        public SearchCommand(DownloadViewModelList src)
        {
            _src = src;
        }
        public void Execute(object arg)
        {
            var model = new DownloadViewModel(_src, (arg as Xamarin.Forms.Entry).Text);
            _src.Items.Add(model);
        }
        public bool CanExecute(object arg)
        {
            return Uri.IsWellFormedUriString((arg as Xamarin.Forms.Entry)?.Text, UriKind.RelativeOrAbsolute);
        }
    }
    public class RemoveCommand : ICommand
    {
        private DownloadViewModelList _src;
        public event EventHandler CanExecuteChanged;
        public RemoveCommand(DownloadViewModelList src)
        {
            _src = src;
        }
        public void Execute(object arg)
        {
            _src.Items.Remove(arg as DownloadViewModel);
        }
        public bool CanExecute(object arg)
        {
            return arg != null;
        }
    }
}
