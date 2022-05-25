using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
namespace Extender.Converters
{
    public class MethodImageConverter:IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return (path as string) == Resource.PostTitle || (path as string) == Resource.PutTitle;
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            throw new NotImplementedException();
        }
    }
}
