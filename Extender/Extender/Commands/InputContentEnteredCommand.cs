using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Extender.ViewModels;
using Extender.Abstractions;
namespace Extender.Commands
{
    public struct InputContentEnteredCommand : IUpdatableCommand
    {
        private readonly HttpRequestViewModel _viewModel;
        private readonly IUpdatableCommand _searchCommand;
        public InputContentEnteredCommand(HttpRequestViewModel viewModel,IUpdatableCommand searchCommand)
        {
            _viewModel = viewModel;
            _searchCommand = searchCommand;
            CanExecuteChanged = (s, a) =>
            {

            };
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object arg)
        {
            _viewModel.IsJsonContent = true;
            _viewModel.HttpContent = Encoding.Default.GetBytes((arg as Entry)?.Text);
            _searchCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Entry != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
