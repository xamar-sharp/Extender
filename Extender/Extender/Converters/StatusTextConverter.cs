using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Net.Http;
using System.Globalization;
namespace Extender.Converters
{
    public class StatusTextConverter:IValueConverter
    {
        public object Convert(object path, Type type, object param, CultureInfo uiCulture)
        {
            return ((path as HttpResponseMessage).IsSuccessStatusCode ? Resource.SuccessHttpResponseTitle : Resource.FailedHttpResponseTitle)+":"+(int)(path as HttpResponseMessage).StatusCode;
        }
        public object ConvertBack(object path, Type type, object param, CultureInfo uiCulture)
        {
            throw new NotImplementedException();
        }
    }
}
