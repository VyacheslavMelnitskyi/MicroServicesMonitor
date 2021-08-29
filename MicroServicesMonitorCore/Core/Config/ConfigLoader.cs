using System.IO;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Core.Config;
using Newtonsoft.Json;

namespace MicroServicesMonitorCore.Core
{
    public class ConfigLoader
    {
        public async Task<SettingsConfig> LoadDataAsync(string fullFileName)
        {
            var fileData =  await File.ReadAllTextAsync(fullFileName);
            var config = JsonConvert.DeserializeObject<SettingsConfig>(fileData);
            
            return config;
        }

        public SettingsConfig LoadData(string fullFileName)
        {
            var fileData = File.ReadAllText(fullFileName);
            var config = JsonConvert.DeserializeObject<SettingsConfig>(fileData);
            return config;
        }

    }
}
