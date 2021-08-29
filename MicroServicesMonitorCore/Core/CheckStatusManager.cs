using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Core.Interfaces;
using MicroServicesMonitorCore.Enums;

namespace MicroServicesMonitorCore.Core
{
    public class CheckStatusManager
    {
        private readonly ICheckStatus _checkStatusViaUrl;

        private readonly ICheckStatus _checkStatusViaWindowText;

        private readonly ICheckStatus _checkStatusViaProcessName;

        public CheckStatusManager(ICheckStatus checkStatusViaUrl, ICheckStatus checkStatusViaWindowText, ICheckStatus checkStatusViaProcessName)
        {
            _checkStatusViaUrl = checkStatusViaUrl;
            _checkStatusViaWindowText = checkStatusViaWindowText;
            _checkStatusViaProcessName = checkStatusViaProcessName;
        }

        private static object _locker = new object();

        public async Task<WorkingServices> CheckStatusesAsync(WorkingServices services)
        {

            var taskList = new List<Task>();
            foreach (var service in services.Items)
            {
                taskList.Add( Task.Run(async () =>
                {
                    var checkStatus = GetChecker(service.CheckType);
                    service.IsOnline = await checkStatus.CheckAsync(service);
                }));
               
            }
            await Task.WhenAll(taskList);

            return services;
        }

        public WorkingServices CheckStatuses(WorkingServices services)
        {
            foreach (var service in services.Items)
            {
                var checkStatus = GetChecker(service.CheckType);

                service.IsOnline = checkStatus.Check(service);
            }

            return services;
        }

        private ICheckStatus GetChecker(CheckType serviceCheckType) =>
            serviceCheckType switch
            {
                CheckType.Http => _checkStatusViaUrl,
                CheckType.WindowTitle => _checkStatusViaWindowText,
                CheckType.ProcessName => _checkStatusViaProcessName,
                _ => throw new NotImplementedException($"checkType = {serviceCheckType} is not implemented!")
            };
    }
}
