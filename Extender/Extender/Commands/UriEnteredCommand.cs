using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Extender.Abstractions;
using Extender.ViewModels;
using Xamarin.Forms;
namespace Extender.Commands
{
    public struct UriEnteredCommand : IUpdatableCommand
    {
        private readonly HttpRequestViewModel _viewModel;
        private readonly IUpdatableCommand _searchCommand;
        public UriEnteredCommand(HttpRequestViewModel viewModel,IUpdatableCommand searchCommand)
        {
            _viewModel = viewModel;
            CanExecuteChanged = (s, a) => {
          
            };
            _searchCommand = searchCommand;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object arg)
        {
            _viewModel.RequestUri = (arg as Entry)?.Text;
            _searchCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return Uri.IsWellFormedUriString((arg as Entry)?.Text, UriKind.Absolute);
        }
        public event EventHandler CanExecuteChanged;
    }
}
