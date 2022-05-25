using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Extender.Abstractions;
using Extender.Commands;
using Xamarin.Forms;
namespace Extender.ViewModels
{
    public class HttpRequestViewModel : INotifyPropertyChanged
    {
        private string _requestUri;
        private HttpMethod _httpMethod;
        private string _token;
        private byte[] _httpContent;
        private bool _isJsonContent;
        private static HttpClient _client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }) { MaxResponseContentBufferSize = int.MaxValue, Timeout = TimeSpan.FromMinutes(2) };
        private static CancellationTokenSource _source = new CancellationTokenSource();
        public string RequestUri
        { get => _requestUri; set { _requestUri = value; OnPropertyChanged(); } }
        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                OnPropertyChanged();
            }
        }
        public bool IsJsonContent
        {
            get => _isJsonContent;
            set
            {
                _isJsonContent = value;
                OnPropertyChanged();
            }
        }
        public byte[] HttpContent { get => _httpContent; set { _httpContent = value; OnPropertyChanged(); } }
        public HttpMethod HttpRequestMethod
        {
            get => _httpMethod;
            set
            {
                _httpMethod = value;
                OnPropertyChanged();
            }
        }
        public IUpdatableCommand UriEnteredCommand { get; set; }
        public IUpdatableCommand MethodChangedCommand { get; set; }
        public IUpdatableCommand AuthTokenEnteredCommand { get; set; }
        public IUpdatableCommand InputContentEnteredCommand { get; set; }
        public IUpdatableCommand SelectFileCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public HttpRequestViewModel(IUpdatableCommand searchCommand)
        {
            UriEnteredCommand = new UriEnteredCommand(this, searchCommand);
            MethodChangedCommand = new MethodChangedCommand(this, searchCommand);
            AuthTokenEnteredCommand = new AuthTokenEnteredCommand(this, searchCommand);
            InputContentEnteredCommand = new InputContentEnteredCommand(this, searchCommand);
            SelectFileCommand = new SelectFileCommand(this, searchCommand);
        }
        public async ValueTask<HttpResponseMessage> ExecuteAsync()
        {
            try
            {
                HttpContent content;
                //_source.CancelAfter(TimeSpan.FromSeconds(100));
                if (Token != null)
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                switch (_httpMethod)
                {
                    case HttpMethod.Get:
                        return await _client.GetAsync(_requestUri, HttpCompletionOption.ResponseContentRead, _source.Token);
                    case HttpMethod.Post:
                        if (_isJsonContent)
                        {
                            content = new StringContent(Encoding.Default.GetString(_httpContent), Encoding.UTF8, "application/json");
                        }
                        else
                        {
                            content = new ByteArrayContent(_httpContent);
                        }
                        return await _client.PostAsync(_requestUri, content, _source.Token);
                    case HttpMethod.Put:
                        if (_isJsonContent)
                        {
                            content = new StringContent(Encoding.Default.GetString(_httpContent), Encoding.UTF8, "application/json");
                        }
                        else
                        {
                            content = new ByteArrayContent(_httpContent);
                        }
                        return await _client.PutAsync(_requestUri, content, _source.Token);
                    case HttpMethod.Delete:
                        return await _client.DeleteAsync(_requestUri, _source.Token);
                }
            }
            catch (Exception ex)
            {
                _client.CancelPendingRequests();
            }
            return null;
        }
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            UriEnteredCommand.ChangeCanExecute();
            SelectFileCommand.ChangeCanExecute();
            MethodChangedCommand.ChangeCanExecute();
            InputContentEnteredCommand.ChangeCanExecute();
            AuthTokenEnteredCommand.ChangeCanExecute();
        }
        public enum HttpMethod
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
