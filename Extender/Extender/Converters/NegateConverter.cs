using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
using Extender.ViewModels;
namespace Extender.Converters
{
    public class NegateConverter : IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return !(bool)path;
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            return !(bool)path;
        }
    }
}
