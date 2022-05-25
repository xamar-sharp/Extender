using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Extender.Abstractions;
using Xamarin.Essentials;
using System.IO;
using Extender.ViewModels;
namespace Extender.Commands
{
    public class SelectFileCommand : IUpdatableCommand
    {
        private readonly HttpRequestViewModel _viewModel;
        private readonly IUpdatableCommand _searchCommand;
        public SelectFileCommand(HttpRequestViewModel viewModel,IUpdatableCommand searchCommand)
        {
            _viewModel = viewModel;
            CanExecuteChanged = (s, a) =>
            {

            };
            _searchCommand = searchCommand;
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object arg)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                using (FileStream stream = (FileStream)await (await FilePicker.PickAsync()).OpenReadAsync())
                {
                    (arg as Entry).Text = stream.Name;
                    _viewModel.IsJsonContent = false;
                    _viewModel.HttpContent = new byte[stream.Length];
                    await stream.ReadAsync(_viewModel.HttpContent);
                }
                _searchCommand.ChangeCanExecute();
            });
        }
        public bool CanExecute(object arg)
        {
            return arg as Entry != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
