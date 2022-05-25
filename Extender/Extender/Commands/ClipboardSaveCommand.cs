using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Net.Http;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Extender.Abstractions;
namespace Extender.Commands
{
    public struct ClipboardSaveCommand:IUpdatableCommand
    {
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public async void Execute(object arg)
        {
            StringBuilder builder = new StringBuilder(512);
            var contentString = await (arg as HttpResponseMessage).Content.ReadAsStringAsync();
            builder.AppendLine($"{Resource.ResponseUriTitle} : {(arg as HttpResponseMessage).RequestMessage.RequestUri} &&&");
            builder.AppendLine($"{Resource.ResponseContentTitle} : {contentString} &&&");
            builder.AppendLine($"{Resource.ResponseStatusCodeTitle} : {(arg as HttpResponseMessage).StatusCode} &&&");
            builder.AppendLine($"{Resource.ResponseHeadersTitle} : {JsonConvert.SerializeObject((arg as HttpResponseMessage).Headers)} ;;;");
            await Clipboard.SetTextAsync(builder.ToString());
        }
        public bool CanExecute(object arg)
        {
            return arg as HttpResponseMessage != null;
        }
        public event EventHandler CanExecuteChanged;
    }
}
