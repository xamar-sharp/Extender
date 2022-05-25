using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace Extender.ViewModels
{
    public class MailViewModel:INotifyPropertyChanged
    {
        private double _length;
        private string _extension;
        private string _fileName;
        public double Length { get { return _length; } set { _length = value; OnPropertyChanged(); } }
        public string Extension { get { return _extension; } set { _extension = value; OnPropertyChanged(); } }
        public bool Supports { get => Mappings.MimeTypes.ContainsKey(Extension); }
        public string FileName { get { return _fileName; } set { _fileName = value; OnPropertyChanged(); } }
        public string PhysicalPath { get; set; }
        public MailViewModelList Parent { get; set; }
        public MailViewModel(MailViewModelList parent)
        {
            Parent = parent;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = default)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
