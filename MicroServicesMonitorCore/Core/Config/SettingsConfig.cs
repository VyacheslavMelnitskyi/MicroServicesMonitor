using System.Collections.Generic;

namespace MicroServicesMonitorCore.Core.Config
{
    public class SettingsConfig
    {
        public bool Transparent { get; set; }
        public bool TopMost { get; set; }
        public bool TimerStart { get; set; }

        public List<ConfigDataItem> MicroServiceConfigs { get; set; }

    }
}
