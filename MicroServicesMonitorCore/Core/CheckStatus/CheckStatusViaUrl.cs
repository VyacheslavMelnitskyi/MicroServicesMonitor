using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MicroServicesMonitorCore.Core.Interfaces;

namespace MicroServicesMonitorCore.Core.CheckStatus
{
    public class CheckStatusViaUrl:ICheckStatus
    {
        private const int TimeoutSec = 5;

        public async Task<bool> CheckAsync(WorkingServiceDataItem service)
        {
            try
            {
                var http = GetHttpClient(service);
                var response = await http.SendAsync(new HttpRequestMessage(HttpMethod.Get, service.Url));
                var isOnline = response.StatusCode == HttpStatusCode.OK;
                return isOnline;
            }
            catch 
            {
                //todo Log
                return false;
            }
        }

        public bool Check(WorkingServiceDataItem service)
        {
            try
            {
                var http = GetHttpClient(service);
                var response = http.SendAsync(new HttpRequestMessage(HttpMethod.Get, service.Url)).Result;
                var isOnline = response.StatusCode == HttpStatusCode.OK;
                return isOnline;
            }
            catch
            {
                //todo Log
                return false;
            }
        }

        private static HttpClient GetHttpClient(WorkingServiceDataItem service)
        {
            var http = service.HttpClient ?? new HttpClient {Timeout = new TimeSpan(0, 0, 0, TimeoutSec)};
            service.HttpClient = http;
            return http;
        }
    }
}
