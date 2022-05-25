using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Extender.Abstractions;
namespace Extender
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : Shell, IThemeChanger, ICacheWorker
    {
        public string NetworkPageTitle { get; set; }
        public string RuntimePageTitle { get; set; }
        public MainMenu()
        {
            InitializeComponent();
            NetworkPageTitle = Resource.NetworkPageTitle;
            RuntimePageTitle = Resource.RuntimePageTitle;
            this.BindingContext = this;
            
        }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNightTheme)
        {
            if (useNightTheme || DateTime.Now.Hour >= 18)
            {
                (FlyoutFooter as Label).SetDynamicResource(StyleProperty, "NightLabel");
                ItemTemplate = new DataTemplate(() =>
                {
                    StackLayout layout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical
                    };
                    Image image = new Image() { Aspect = Aspect.Fill };
                    image.SetBinding(Image.SourceProperty, "Icon");
                    image.SetDynamicResource(StyleProperty, "NightImage");
                    Frame frame = new Frame()
                    {
                        Content = image,
                        Padding = new Thickness(0),
                        CornerRadius = 90,
                        HeightRequest = 90,
                        WidthRequest = 90
                    };
                    frame.SetDynamicResource(StyleProperty, "NightFrame");
                    Label label = new Label()
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    };
                    label.SetDynamicResource(StyleProperty, "NightLabel");
                    label.SetBinding(Label.TextProperty, "Title");
                    layout.SetDynamicResource(StyleProperty, "NightStackLayout");
                    layout.Children.Add(frame);
                    layout.Children.Add(label);
                    return layout;
                });
                MenuItemTemplate = new DataTemplate(() =>
                {
                    StackLayout layout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical
                    };
                    Image image = new Image() { Aspect = Aspect.Fill };
                    image.SetBinding(Image.SourceProperty, "Icon");
                    image.SetDynamicResource(StyleProperty, "NightImage");
                    Frame frame = new Frame()
                    {
                        Content = image,
                        Padding = new Thickness(0),
                        CornerRadius = 90,
                        HeightRequest = 48,
                        WidthRequest = 48
                    };
                    frame.SetDynamicResource(StyleProperty, "NightFrame");
                    Label label = new Label()
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    };
                    label.SetDynamicResource(StyleProperty, "NightLabel");
                    label.SetBinding(Label.TextProperty, "Title");
                    layout.SetDynamicResource(StyleProperty, "NightStackLayout");
                    layout.Children.Add(frame);
                    layout.Children.Add(label);
                    return layout;
                });
                FlyoutHeader = new Image() { Source = "headericon.jpg", Aspect = Aspect.Fill, HeightRequest = 200, WidthRequest = 300 };
            }
            else
            {
                (FlyoutFooter as Label).SetDynamicResource(StyleProperty, "DayLabel");
                ItemTemplate = new DataTemplate(() =>
                {
                    StackLayout layout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical
                    };
                    Image image = new Image() { Aspect = Aspect.Fill };
                    image.SetBinding(Image.SourceProperty, "Icon");
                    image.SetDynamicResource(StyleProperty, "DayImage");
                    Frame frame = new Frame()
                    {
                        Content = image,
                        Padding = new Thickness(0),
                        CornerRadius = 90,
                        HeightRequest = 90,
                        WidthRequest = 90
                    };
                    frame.SetDynamicResource(StyleProperty, "DayFrame");
                    Label label = new Label()
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    };
                    label.SetDynamicResource(StyleProperty, "DayLabel");
                    label.SetBinding(Label.TextProperty, "Title");
                    layout.SetDynamicResource(StyleProperty, "DayStackLayout");
                    layout.Children.Add(frame);
                    layout.Children.Add(label);
                    return layout;
                });
                MenuItemTemplate = new DataTemplate(() =>
                {
                    StackLayout layout = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical
                    };
                    Image image = new Image() { Aspect = Aspect.Fill };
                    image.SetBinding(Image.SourceProperty, "Icon");
                    image.SetDynamicResource(StyleProperty, "DayImage");
                    Frame frame = new Frame()
                    {
                        Content = image,
                        Padding = new Thickness(0),
                        CornerRadius = 90,
                        HeightRequest = 48,
                        WidthRequest = 48
                    };
                    frame.SetDynamicResource(StyleProperty, "DayFrame");
                    Label label = new Label()
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    };
                    label.SetDynamicResource(StyleProperty, "DayLabel");
                    label.SetBinding(Label.TextProperty, "Title");
                    layout.SetDynamicResource(StyleProperty, "DayStackLayout");
                    layout.Children.Add(frame);
                    layout.Children.Add(label);
                    return layout;
                });
                FlyoutHeader = new Image() { Source = "headerday.jpg", Aspect = Aspect.Fill, HeightRequest = 200, WidthRequest = 300 };
            }
        }
    }
}