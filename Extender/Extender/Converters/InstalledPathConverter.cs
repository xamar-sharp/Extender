using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
namespace Extender.Converters
{
    public class InstalledPathConverter : IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return Resource.InstalledPath + ":" + path;
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            return Resource.InstalledPath + ":" + path;
        }
    }
}
