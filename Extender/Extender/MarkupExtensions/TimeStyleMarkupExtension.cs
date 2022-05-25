using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Reflection;
using Extender.Abstractions;
using Xamarin.Essentials;
namespace Extender.MarkupExtensions
{
    [ContentProperty("Element")]
    public class TimeStyleMarkupExtension : IMarkupExtension, IThemeChanger, ICacheWorker
    {
        public View Element { get; set; }
        private string State { get; set; }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNight)
        {
            if (useNight || DateTime.Now.Hour >= 18)
            {
                State = "Night";
            }
            else
            {
                State = "Day";
            }
        }
        public object ProvideValue(IServiceProvider provider)
        {
            SetTheme(CheckCache("Theme"));
            var name= Element.GetType().Name;
            Element.SetDynamicResource(View.StyleProperty, string.Concat(State ?? "Day",name));
            return null;
        }
    }
}
