using System;
using System.Net;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Interfaces
{
    public interface IPingService
    {
        Task<bool> SendWithRetry(IPAddress ip, int retryCount, TimeSpan interval);
        Task<bool> Send(IPAddress ip);
    }
}