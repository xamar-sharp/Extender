using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Globalization;
namespace Extender.Converters
{
    public class SuccessLoadConverter : IValueConverter
    {
        public object Convert(object path, Type targetType, object arg, CultureInfo info)
        {
            var model = path as ViewModels.DownloadViewModel;
            if(model is null || model.Exceptional)
            {
                return Device.GetNamedColor(NamedPlatformColor.HoloRedLight);
            }
            else
            {
                return Device.GetNamedColor(NamedPlatformColor.HoloGreenLight);
            }
        }
        public object ConvertBack(object path, Type targetType, object arg, CultureInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
