using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Extender.ViewModels;
using Extender.Abstractions;
using Xamarin.Essentials;
using Extender.Implementations;
using System.Windows.Input;
namespace Extender
{
    public partial class ProcessPage : ContentPage, IThemeChanger, ICacheWorker, IProcessPage
    {
        public ICommand RefreshCommand { get; set; }
        public ProcessFilter ProcessFilter { get; set; }
        public ICommand EnterToStartProcessCommand { get; set; }
        public ProcessPage()
        {
            InitializeComponent();
            SetTheme(CheckCache("Theme"));
            Title = Resource.ProcessPageTitle;
            IconImageSource = "processicon.png";
            var filterProvider = new ProcessFilterProvider(new Dictionary<ProcessFilter, IProcessFilter>());
            var processFormatter = new ProcessFormatter();
            var processManipulator = new DefaultProcessManipulator();
            EnterToStartProcessCommand = new Command(async () =>
              {
                  await Navigation.PushAsync(new ProcessStartPage(processManipulator));
              });
            RefreshCommand = new RefreshProcessCommand(filterProvider, processFormatter, ProcessFilter, this, new DefaultProcessManipulator(), this, 310);
            RefreshCommand.Execute(statistic);
            foreach (var key in Enum.GetNames(typeof(ProcessFilter)))
                filterPicker.Items.Add(key);
            title.Text = Resource.ProcessTitle;
            startProcess.Text = Resource.ProcessStart;
            this.BindingContext = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCommand?.Execute(statistic);
        }
        public bool CheckCache(string key)
        {
            return Preferences.ContainsKey(key) && Preferences.Get(key, "Day") == "Night";
        }
        public void SetTheme(bool useNightTheme)
        {
            if (useNightTheme || DateTime.Now.Hour >= 18)
            {
                startProcess.SetDynamicResource(StyleProperty, "NightButton");
                scroll.SetDynamicResource(StyleProperty, "NightScrollView");
                root.SetDynamicResource(StyleProperty, "NightStackLayout");
                statistic.SetDynamicResource(StyleProperty, "NightFlexLayout");
                refresher.SetDynamicResource(StyleProperty, "NightRefreshView");
            }
            else
            {
                startProcess.SetDynamicResource(StyleProperty, "DayButton");
                scroll.SetDynamicResource(StyleProperty, "DayScrollView");
                root.SetDynamicResource(StyleProperty, "DayStackLayout");
                statistic.SetDynamicResource(StyleProperty, "DayFlexLayout");
                refresher.SetDynamicResource(StyleProperty, "DayRefreshView");
            }
        }

        private void filterPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(filterPicker.SelectedItem as string, out ProcessFilter filter);
            ProcessFilter = filter;
            RefreshCommand.Execute(statistic);
        }
    }
    public enum ProcessFilter
    {
        VirtualMemory = 0, PhysicalMemory = 1, Priority = 2, Time = 4
    }
    public interface IProcessFilterProvider
    {
        Process[] GetProcessesByFilter(ProcessFilter filter);
        float GetPart(Process inputProcess, Process[] processes, ProcessFilter filter);
    }
    public interface IProcessFilter
    {
        Process[] GetProcesses();
        float GetPart(object value, Process[] processes);
    }
    public class ProcessFilterProvider : IProcessFilterProvider
    {
        private Dictionary<ProcessFilter, IProcessFilter> _filtersMap;
        public ProcessFilterProvider(IDictionary<ProcessFilter, IProcessFilter> defaults)
        {
            _filtersMap = new Dictionary<ProcessFilter, IProcessFilter>(defaults)
            {
                [ProcessFilter.PhysicalMemory] = new PhysicalMemoryProcessFilter(),
                [ProcessFilter.VirtualMemory] = new VirtualMemoryProcessFilter(),
                [ProcessFilter.Time] = new TimeProcessFilter(),
                [ProcessFilter.Priority] = new PriorityProcessFilter()
            };
        }
        public Process[] GetProcessesByFilter(ProcessFilter filter)
        {
            return _filtersMap[filter].GetProcesses();
        }
        public float GetPart(Process inputProcess, Process[] processes, ProcessFilter filter)
        {
            return _filtersMap[filter].GetPart(DetectProperty(inputProcess, filter), processes);
        }
        private object DetectProperty(Process inputProcess, ProcessFilter filter)
        {
            switch (filter)
            {
                case ProcessFilter.VirtualMemory:
                    return inputProcess.VirtualMemorySize64;
                case ProcessFilter.PhysicalMemory:
                    return inputProcess.WorkingSet64;
                case ProcessFilter.Priority:
                    return inputProcess.BasePriority == 0 ? 1 : inputProcess.BasePriority;
                case ProcessFilter.Time:
                    try
                    {
                        return inputProcess.StartTime.Ticks;
                    }
                    catch
                    {
                        return 1;
                    }
            }
            return 0;
        }
    }
    public struct VirtualMemoryProcessFilter : IProcessFilter
    {
        public Process[] GetProcesses()
        {
            return Process.GetProcesses().OrderBy(process => process.VirtualMemorySize64).ToArray();
        }
        public float GetPart(object value, Process[] processes)
        {
            long maximum = processes.Max(process => process.VirtualMemorySize64) - processes.Min(process => process.VirtualMemorySize64);
            return (long)value / (maximum == 0 ? (long)value : maximum);
        }
    }
    public struct PhysicalMemoryProcessFilter : IProcessFilter
    {
        public Process[] GetProcesses()
        {
            return Process.GetProcesses().OrderBy(process => process.WorkingSet64).ToArray();
        }
        public float GetPart(object value, Process[] processes)
        {
            long maximum = processes.Max(process => process.WorkingSet64) - processes.Min(process => process.WorkingSet64);
            return (long)value / (maximum == 0 ? (long)value : maximum);
        }
    }
    public struct PriorityProcessFilter : IProcessFilter
    {
        public Process[] GetProcesses()
        {
            return Process.GetProcesses().OrderBy(process => process.BasePriority).ToArray();
        }
        public float GetPart(object value, Process[] processes)
        {
            long maximum = processes.Max(process => process.BasePriority) - processes.Min(process => process.BasePriority);
            return Convert.ToInt64(value) / (maximum == 0 ? Convert.ToInt64(value) : maximum);
        }
    }
    public struct TimeProcessFilter : IProcessFilter
    {
        public Process[] GetProcesses()
        {
            return Process.GetProcesses().OrderBy(process => { try { return process.StartTime.Ticks; } catch { return 0; } }).ToArray();
        }
        public float GetPart(object value, Process[] processes)
        {
            long maximum = processes.Max(process =>
             {
                 try
                 {
                     return process.StartTime.Ticks;
                 }
                 catch
                 {
                     return 1;
                 }
             }) - processes.Min(process =>
             {
                 try
                 {
                     return process.StartTime.Ticks;
                 }
                 catch
                 {
                     return 0;
                 }
             });
            return (long)value / (maximum == 0 ? (long)value : maximum);
        }
    }
    public class RefreshProcessCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IProcessFilterProvider _filterProvider;
        private ProcessFilter _filter;
        private Random _randomColorGenerator;
        private Page _syncPage;
        private ProcessManipulator _processManipulator;
        private IProcessPage _page;
        private IProcessFormatter _formatter;
        private int _maxWidth;
        public RefreshProcessCommand(IProcessFilterProvider filterProvider, IProcessFormatter formatter, ProcessFilter filter, IProcessPage page, ProcessManipulator manipulator, Page syncPage, int maxWidth)
        {
            _maxWidth = maxWidth;
            _randomColorGenerator = new Random();
            _processManipulator = manipulator;
            _filterProvider = filterProvider;
            _formatter = formatter;
            _filter = filter;
            _page = page;
            _syncPage = syncPage;
        }
        public bool CanExecute(object arg)
        {
            return _filterProvider.GetProcessesByFilter(_filter).Length != 0 && arg as FlexLayout != null;
        }
        public void Execute(object arg)
        {
            if (CanExecute(arg))
            {
                _filter = _page.ProcessFilter;
                BoxView tempView;
                StackLayout pair;
                Label procents;
                TapGestureRecognizer recognizer;
                Process[] processes = _filterProvider.GetProcessesByFilter(_filter);
                (arg as FlexLayout).Children.Clear();
                foreach (var process in processes)
                {
                    var val = _filterProvider.GetPart(process, processes, _filter);
                    recognizer = new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1
                    };
                    recognizer.Tapped += (sender, args) =>
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            bool isKill = await _syncPage.DisplayAlert(Resource.ProcessInfo, _formatter.GetInfo(process), Resource.ProcessKill, Resource.CancelTitle);
                            if (isKill)
                            {
                                _processManipulator.Kill(process);
                            }
                        });
                    };
                    tempView = new BoxView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Color = new Color(_randomColorGenerator.NextDouble(),
    _randomColorGenerator.NextDouble(), _randomColorGenerator.NextDouble()),
                        WidthRequest = val * _maxWidth
                    };
                    procents = new Label()
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = $"{val * 100}%",
                    };
                    procents.SetDynamicResource(VisualElement.StyleProperty, "DayLabel");
                    pair = new StackLayout()
                    {
                        Children = { tempView, procents },
                        Orientation = StackOrientation.Horizontal,
                        GestureRecognizers = { recognizer }
                    };
                    pair.SetDynamicResource(VisualElement.StyleProperty, "DayStackLayout");

                    (arg as FlexLayout).Children.Add(pair);
                }
            }
        }
    }
}
