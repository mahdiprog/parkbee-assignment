using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Infra
{
    public class PingService : IPingService
    {

        private readonly Ping _pingSender;
        public PingService()
        {
            _pingSender = new Ping ();

        }

        public async Task<bool> SendWithRetry(IPAddress ip, int retryCount, TimeSpan interval)
        {
            var exceptions = new List<Exception>();
            for (int attempted = 0; attempted < retryCount; attempted++)
            {
                try
                {
                    // Call external service.
                    bool successful = await Send(ip);
                    if (successful)
                        return successful;
                    // Wait to retry the operation.
                    await Task.Delay(interval);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }

            }

            if (exceptions.Any())
                throw new AggregateException(exceptions);
            return false;
        }

        public async Task<bool> Send(IPAddress ip)
        {
            PingReply reply = await _pingSender.SendPingAsync(ip);
            return (reply.Status == IPStatus.Success);
        }
       
    }
}
