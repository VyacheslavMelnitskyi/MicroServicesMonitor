using System.Diagnostics;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Core.Interfaces;

namespace MicroServicesMonitorCore.Core.CheckStatus
{
    public class CheckStatusViaWindowTitle : ICheckStatus
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
                
                Parallel.ForEach(processlist, (process) =>
                {
                    if (process.MainWindowTitle.Contains(service.Name))
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
