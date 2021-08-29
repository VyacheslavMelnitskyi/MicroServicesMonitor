using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MicroServicesMonitor.Classes;
using MicroServicesMonitorCore.Core;
using MicroServicesMonitorCore.Core.CheckStatus;
using MicroServicesMonitorCore.Core.Config;
using MicroServicesMonitorCore.View;

namespace MicroServicesMonitor
{
    public partial class MainForm : Form
    {
        private CheckStatusManager _checkStatusManager;
        private WorkingServices _servicesData;
        private List<Control> _controls;
        private ControlsBuilder _builder;
        private CalcControls _calcControls;
        private ServiceManager _serviceManager;
        public MainForm()
        {
            InitializeComponent();
            Opacity = 0.0D;
        }

        private async Task Init()
        {
            var configData = await GetConfigData();
            _servicesData = new WorkingServices(configData);
            _controls = InitControls(_servicesData);
            _servicesData.AnyItemStatusChanged += ServicesData_AnyItemStatusChanged;
            _checkStatusManager = new CheckStatusManager(new CheckStatusViaUrl(), new CheckStatusViaWindowTitle(), new CheckStatusViaProcessName());
            InitDefaults(configData);
        }

        private void ServicesData_AnyItemStatusChanged(WorkingServiceDataItem senderService, StatusChangedEventArg arg)
        {
            Application.DoEvents();
            Invoke((Action)(() =>
            {
                var updateElement = _calcControls.UpdateElement(senderService);
                _builder.UpdateControl(_controls, updateElement);
                EnableCheckButton(true);
            }));
        }

        private void InitDefaults(SettingsConfig config)
        {
            TopMostCheckBox.Checked = config.TopMost;
            TopMost = config.TopMost;
            TransparentCheckBox.Checked = config.Transparent;
            SetTransparent(config.Transparent);
            TimerCheckBox.Checked = config.TimerStart;
            CheckStatusTimer.Enabled= config.TimerStart;
        }

        private List<Control> InitControls(WorkingServices configData)
        {
            
            _calcControls = new CalcControls();
            var elements = _calcControls.CalcElements(configData);
            
            _serviceManager = new ServiceManager(configData);

            _builder = new ControlsBuilder(_serviceManager);
            var controls = _builder.Build(elements, this);

            SetStartPosition();

            return controls;

        }

        private void SetStartPosition()
        {
            Screen screen = Screen.PrimaryScreen;
            int screenWidth = screen.Bounds.Width;
            Location = new Point(screenWidth - Width - 40, 60);
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
            Application.DoEvents();
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
        }
        
        private void TopMostCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = TopMostCheckBox.Checked;
        }

        private void TransparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetTransparent(TransparentCheckBox.Checked);
        }

        private void SetTransparent(bool isTransparent)
        {
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
            CheckStatusTimer.Enabled = TimerCheckBox.Checked;
        }

        private async void CheckServicesButton_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            EnableCheckButton(false);
            await _checkStatusManager.CheckStatusesAsync(_servicesData);

        }

        private void EnableCheckButton(bool isEnable)
        {
            CheckServicesButton.Invoke((MethodInvoker) (() => { CheckServicesButton.Enabled = isEnable; }));
            EnableCheckButtonTimer.Enabled = !isEnable;
        }

        private void EnableCheckButtonTimer_Tick(object sender, EventArgs e)
        {
           EnableCheckButton(true);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Init();
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CheckStatusTimer.Enabled)
            {
                CheckStatusTimer.Stop();
            }

            _servicesData.AnyItemStatusChanged -= ServicesData_AnyItemStatusChanged;
        }

        private async void StartAllButton_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            StartAllButton.Enabled = false;
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
            _serviceManager.StartAllServices();
            StartAllButton.Enabled = true;

        }

        private async void StopAllButton_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            StopAllButton.Enabled = false;
            await _checkStatusManager.CheckStatusesAsync(_servicesData);
            _serviceManager.StopAllServices();
            StopAllButton.Enabled = true;
        }
    }
}
