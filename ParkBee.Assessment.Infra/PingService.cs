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

        private readonly int _retryCount;
        private readonly TimeSpan _interval;
        private readonly Ping _pingSender;
        public PingService(IConfiguration configuration)
        {
            if(!TimeSpan.TryParse(configuration["Retry:Interval"], out _interval))
                _interval = TimeSpan.FromSeconds(2);
            if(!Int32.TryParse(configuration["Retry:Count"], out _retryCount))
                _retryCount = 2;
            _pingSender = new Ping ();

        }

        public async Task<bool> SendWithRetry(IPAddress ip)
        {
            var exceptions = new List<Exception>();
            for (int attempted = 0; attempted < _retryCount; attempted++)
            {
                try
                {
                    // Call external service.
                    bool successful = await Send(ip);
                    if (successful)
                        return successful;
                    // Wait to retry the operation.
                    await Task.Delay(_interval);
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
