using System;
using System.Collections.Generic;
using System.Text;
using Extender.Implementations;
using Extender.Abstractions;
using System.Windows.Input;
using Extender.ViewModels;
using System.IO;
using Xamarin.Essentials;
namespace Extender.Commands
{
    public class MailAddCommand:ICommand
    {
        private readonly MailViewModelList _list;
        public event EventHandler CanExecuteChanged;
        public MailAddCommand(MailViewModelList list)
        {
            _list = list;
        }
        public async void Execute(object arg)
        {
            IEnumerable<FileResult> results = await FilePicker.PickMultipleAsync();
            if(results != null)
            {
                foreach(var result in results)
                {
                    string extension = Path.GetExtension(result.FullPath);
                    _list.Mails.Add(new MailViewModel(_list)
                    {
                        Extension = extension,
                        FileName = Path.GetFileNameWithoutExtension(result.FullPath),
                        Length = File.ReadAllBytes(result.FullPath).Length / (double)(1024*1024),
                        PhysicalPath=result.FullPath
                    });
                }
            }
        }
        public bool CanExecute(object arg)
        {
            return true;
        }
    }
}
