using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using System.Net.Http;
namespace Extender.Converters
{
    public class ContentImageConverter:IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return Mappings.ImageTypes.Contains((path as HttpResponseMessage).Content.Headers.ContentType.MediaType);
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            throw new NotImplementedException();
        }
    }
}
