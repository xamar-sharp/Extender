using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Extender.Abstractions;
using Extender.ViewModels;
namespace Extender.Commands
{
    public struct MethodChangedCommand : IUpdatableCommand
    {
        private readonly HttpRequestViewModel _viewModel;
        private readonly IUpdatableCommand _searchCommand;
        public MethodChangedCommand(HttpRequestViewModel viewModel,IUpdatableCommand searchCommand)
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
            string txt = (arg as Button).Text;
            if (txt == Resource.GetTitle)
            {
                (arg as Button).Text = Resource.PostTitle;
                _viewModel.HttpRequestMethod = HttpRequestViewModel.HttpMethod.Post;
            }
            else if (txt == Resource.PostTitle)
            {
                (arg as Button).Text = Resource.PutTitle;
                _viewModel.HttpRequestMethod = HttpRequestViewModel.HttpMethod.Put;
            }
            else if (txt == Resource.PutTitle)
            {
                (arg as Button).Text = Resource.DeleteTitle;
                _viewModel.HttpRequestMethod = HttpRequestViewModel.HttpMethod.Delete;
            }
            else if (txt == Resource.DeleteTitle)
            {
                (arg as Button).Text = Resource.GetTitle;
                _viewModel.HttpRequestMethod = HttpRequestViewModel.HttpMethod.Get;
            }
            _searchCommand.ChangeCanExecute();
        }
        public bool CanExecute(object arg)
        {
            return arg as Button != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
