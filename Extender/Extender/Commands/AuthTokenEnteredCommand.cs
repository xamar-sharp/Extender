using System;
using System.Collections.Generic;
using System.Text;
using Extender.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using Extender.Abstractions;
namespace Extender.Commands
{
    public struct AuthTokenEnteredCommand:IUpdatableCommand
    {
        private readonly HttpRequestViewModel _viewModel;
        private readonly IUpdatableCommand _searchCommand;
        public AuthTokenEnteredCommand(HttpRequestViewModel viewModel,IUpdatableCommand searchCommand)
        {
            _viewModel = viewModel;
            _searchCommand = searchCommand;
            CanExecuteChanged = (s, a) =>
            {

            };
        }
        public void Execute(object arg)
        {
            _viewModel.Token = (arg as Entry).Text;
            _searchCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Entry != null;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler CanExecuteChanged;
    }
}
