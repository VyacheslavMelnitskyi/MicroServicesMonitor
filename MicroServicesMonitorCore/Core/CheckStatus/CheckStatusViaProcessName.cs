using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Core.Interfaces;

namespace MicroServicesMonitorCore.Core.CheckStatus
{
    public class CheckStatusViaProcessName : ICheckStatus
    {
        public  async Task<bool> CheckAsync(WorkingServiceDataItem service)
        {
            return  await Task.FromResult(Check(service));
        }

        public bool Check(WorkingServiceDataItem service)
        {
            var result = false;
            try
            {
                Process[] processlist = Process.GetProcesses();
                
                var serviceNameWithoutExt = Path.GetFileNameWithoutExtension(service.Name);

                Parallel.ForEach(processlist, (process) =>
                {
                    if (process.ProcessName.Equals(service.Name, StringComparison.OrdinalIgnoreCase)
                        || process.ProcessName.Equals(serviceNameWithoutExt, StringComparison.OrdinalIgnoreCase))
                    {
                        result = true;
                    }
                });

            }
            catch
            {
                //todo Log
            }
            
            return result;
            
        }
       
    }
}
