using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using System.Net.Http;
namespace Extender.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return (path as HttpResponseMessage).IsSuccessStatusCode ? Device.GetNamedColor(NamedPlatformColor.HoloGreenDark) : Device.GetNamedColor(NamedPlatformColor.HoloRedLight);
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            throw new NotImplementedException();
        }
    }
}
