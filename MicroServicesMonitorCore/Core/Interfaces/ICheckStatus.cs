using System.Threading.Tasks;

namespace MicroServicesMonitorCore.Core.Interfaces
{
    public interface ICheckStatus
    {
        bool Check(WorkingServiceDataItem service);
        Task<bool> CheckAsync(WorkingServiceDataItem service);
    }
}