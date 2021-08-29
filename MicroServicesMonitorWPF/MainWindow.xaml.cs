using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Core.CheckStatus;
using MicroServicesMonitorCore.Core.Config;
using MicroServicesMonitorCore.View;
using MicroServicesMonitorWPF.Classes;
using Path = System.IO.Path;

namespace MicroServicesMonitorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CheckStatusManager _checkStatusManager;
        private WorkingServices _servicesData;
        private ControlsBuilderBind _builder;
        private CalcControls _calcControls;
        private ServiceManager _serviceManager;
        private readonly DispatcherTimer _checkStatusTimer;
        private readonly DispatcherTimer _enableCheckButtonTimer;
        private ViewElements _elements;

        public MainWindow()
        {
            _checkStatusTimer = new DispatcherTimer();
            _checkStatusTimer.Tick += CheckStatusTimer_Tick;
            _checkStatusTimer.Interval = new TimeSpan(0, 0, 5);

            _enableCheckButtonTimer = new DispatcherTimer();
            _enableCheckButtonTimer.Tick += EnableCheckButtonTimer_Tick;
            _enableCheckButtonTimer.Interval = new TimeSpan(0, 0, 5);

            InitializeComponent();
            
            Width = 0;
            Height = 0;
            WindowStyle = WindowStyle.None;
            //Opacity = 0.0D;
            //Hide();
        }

        private async Task Init()
        {
            var configData = await GetConfigData();
            _servicesData = new WorkingServices(configData);
            InitControls(_servicesData);
            _servicesData.AnyItemStatusChanged += ServicesData_AnyItemStatusChanged;
            _checkStatusManager = new CheckStatusManager(new CheckStatusViaUrl(), new CheckStatusViaWindowTitle(),new CheckStatusViaProcessName());
            InitDefaults(configData);
        }

        private void ServicesData_AnyItemStatusChanged(WorkingServiceDataItem senderService, StatusChangedEventArg arg)
        {
            _calcControls.UpdateElement(senderService, _elements);
            
            Dispatcher.Invoke(() =>
            {
                EnableCheckButton(true);
            });
        }

        private void InitDefaults(SettingsConfig config)
        {
            TopMostCheckBox.IsChecked = config.TopMost;
            Topmost = config.TopMost;
            TransparentCheckBox.IsChecked = config.Transparent;
            SetTransparent(config.Transparent);
            TimerCheckBox.IsChecked = config.TimerStart;
            _checkStatusTimer.IsEnabled = config.TimerStart;
        }

        private void InitControls(WorkingServices configData)
        {
            _calcControls = new CalcControls();
            
            _elements = _calcControls.CalcElements(configData);

            _serviceManager = new ServiceManager(configData);

            _builder = new ControlsBuilderBind(_serviceManager);
            _builder.Build(_elements, this, MainGrid);

            
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            var screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            Left = screenWidth - Width - 40;
            Top = 60;
            
        }

        private async Task<SettingsConfig> GetConfigData()
        {
            var configLoader = new ConfigLoader();

            var fullFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.json");

            var configData = await configLoader.LoadDataAsync(fullFileName);

            return configData;
        }

        private async void CheckStatusTimer_Tick(object sender, EventArgs e)
        {
            
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
        }

       
        private void TopMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Topmost = TopMostCheckBox.IsChecked??false;
        }

        private void TransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetTransparent(TransparentCheckBox.IsChecked ?? false);
        }

        private void SetTransparent(bool isTransparent)
        {
            //AllowsTransparency = isTransparent;
            //MainGrid
            if (isTransparent)
            {
                Opacity = 0.5D;
            }
            else
            {
                Opacity = 1;
            }
        }

        private void TimerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _checkStatusTimer.IsEnabled = TimerCheckBox.IsChecked ?? false;
        }

        private async void CheckServicesButton_Click(object sender, EventArgs e)
        {
            EnableCheckButton(false);
            await _checkStatusManager.CheckStatusesAsync(_servicesData);

        }

        private void EnableCheckButton(bool isEnable)
        {
            CheckServicesButton.IsEnabled = isEnable;
            _enableCheckButtonTimer.IsEnabled = !isEnable;
        }

        private void EnableCheckButtonTimer_Tick(object sender, EventArgs e)
        {
            EnableCheckButton(true);
        }

       
        private async void StartAllButton_Click(object sender, EventArgs e)
        {
            
            StartAllButton.IsEnabled = false;
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
            _serviceManager.StartAllServices();
            StartAllButton.IsEnabled = true;

        }

        private async void StopAllButton_Click(object sender, EventArgs e)
        {
            
            StopAllButton.IsEnabled = false;
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
            _serviceManager.StopAllServices();
            StopAllButton.IsEnabled = true;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Init();
            WindowStyle = WindowStyle.SingleBorderWindow;
            //Opacity = 0.5D;

            await _checkStatusManager.CheckStatusesAsync(_servicesData);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_checkStatusTimer.IsEnabled)
            {
                _checkStatusTimer.Stop();
            }

            _servicesData.AnyItemStatusChanged -= ServicesData_AnyItemStatusChanged;
        }
        
    }
}
