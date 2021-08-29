using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.Core.Config
{
    public class ConfigDataItem
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string StartProcess { get; set; }
        public string StartCommand { get; set; }
        public string StopProcess { get; set; }
        public string StopCommand { get; set; }
        public StopType StopType { get; set; }
        public CheckType CheckType { get; set; }

    }
}
