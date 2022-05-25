using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
namespace Extender.Converters
{
    public class ErrorStateConverter:IValueConverter
    {
        public object Convert(object path,Type targetType,object arg,CultureInfo info)
        {
            return Resource.LoadFileError;
        }
        public object ConvertBack(object path, Type targetType, object arg, CultureInfo info)
        {
            return Resource.LoadFileError;
        }
    }
}
