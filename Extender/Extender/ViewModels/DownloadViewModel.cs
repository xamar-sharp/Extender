using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
namespace Extender.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private double _progress;
        private string _uri;
        private bool _installed;
        private bool _exceptional;
        private string _installedPath;
        private static WebClient _client = new WebClient();
        public double CurrentProgress { get { return _progress; } set { _progress = value; OnPropertyChanged(); } }
        public string Uri { get { return _uri; } set { _uri = value; OnPropertyChanged(); } }
        public bool Installed { get { return _installed; } set { _installed = value; OnPropertyChanged(); } }
        public bool Exceptional { get { return _exceptional; } set { _exceptional = value; OnPropertyChanged(); } }
        public string InstalledPath { get { return _installedPath; } set { _installedPath = value; OnPropertyChanged(); } }
        public DownloadViewModelList Parent { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public DownloadViewModel(DownloadViewModelList parent, string uri)
        {
            Parent = parent;
            CurrentProgress = 0;
            Uri = uri;
            Installed = false;
            Exceptional = false;
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await DownloadFile();
            });
        }
        private async Task DownloadFile()
        {
            try
            {
                var extension = System.IO.Path.GetExtension(Uri);
                var path = System.IO.Path.Combine("/storage/emulated/0/Android/Data/com.companyname.extender", System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetRandomFileName())+(string.IsNullOrWhiteSpace(extension)?".dat":extension));
                CurrentProgress += 0.2;
                await _client.DownloadFileTaskAsync(new Uri(Uri),path );
                Installed = true; 

                CurrentProgress += 0.7;
                InstalledPath = path;
            }
            catch(Exception e)
            {
                Installed = true;
                Exceptional = true;
                InstalledPath = Resource.LoadFileError;
            }

            CurrentProgress += 0.1;
        }
    }
}
