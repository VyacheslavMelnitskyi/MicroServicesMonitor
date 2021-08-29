using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.Core
{
    public class ServiceManager
    {
        private readonly WorkingServices _services;
        public ServiceManager(WorkingServices services)
        {
            _services = services;
        }

        public void DoAction(string id)
        {
            var service = _services.Items.First(x => x.Id == id);

            if (service.IsOnline)
            {
                StopService(service);
            }
            else
            {
               StartService(service);
            }
        }

        public string GetUrl(string id)
        {
            var service = _services.Items.First(x => x.Id == id);
            return service.Url;

        }

        public void StartAllServices()
        {
            var offlineServices = _services.Items.Where(s=>!s.IsOnline).ToList();
            
            foreach (var service in offlineServices)
            {
                Task.Run(() => StartService(service));
            }
           
        }

        public void StopAllServices()
        {
            var onlineServices = _services.Items.Where(s => s.IsOnline).ToList();

            foreach (var service in onlineServices)
            {
                Task.Run(() => StopService(service));
            }
        }

        public void StartService(WorkingServiceDataItem service)
        {
            var proc = RunCommand(service.StartProcess, service.StartCommand);
            service.Proc = proc;
            service.IsOnline = true;
        }

        private static Process RunCommand(string fileName, string command)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = command,
                    RedirectStandardInput = false,
                    RedirectStandardError = false,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    CreateNoWindow = false
                    //,WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                }
            };
            proc.Start();
            return proc;
        }

        public void StopService(WorkingServiceDataItem service)
        {
            service.IsOnline = false;
            Task.Run(()=> StopProcess(service));
        }

        private static void StopProcess(WorkingServiceDataItem service)
        {
            StopViaProcess(service);
            StopViaCommand(service);
        }

        private static void StopViaCommand(WorkingServiceDataItem service)
        {
            if (service.StopType == StopType.Command)
            {
                RunCommand(service.StopProcess,service.StopCommand);
            }
        }

        private static void StopViaProcess(WorkingServiceDataItem service)
        {
            if (service.StopType == StopType.ProcessName)
            {
                if (service.Proc != null)
                {
                    service.Proc.Kill();
                    service.Proc = null;
                }
                else 
                {
                    Process[] processlist = Process.GetProcesses();
                    
                    var serviceNameWithoutExt = Path.GetFileNameWithoutExtension(service.Name);

                    Parallel.ForEach(processlist, (process) =>
                    {
                        if (process.ProcessName.Equals(service.Name, StringComparison.OrdinalIgnoreCase)
                            || process.ProcessName.Equals(serviceNameWithoutExt, StringComparison.OrdinalIgnoreCase))
                        {
                            process.Kill();
                        }
                    });

                }

            }
            else if (service.StopType == StopType.WindowTitle)
            {
                Process[] processlist = Process.GetProcesses();

                Parallel.ForEach(processlist, (process) =>
                {
                    if (process.MainWindowTitle.Contains(service.Name))
                    {
                        process.Kill();
                    }
                });
            }
        }
    }
}
