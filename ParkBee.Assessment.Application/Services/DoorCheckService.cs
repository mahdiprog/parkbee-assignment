using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Services
{
    public class DoorCheckService : IDoorCheckService
    {
        private readonly IPingService _pingService;
        private readonly TimeSpan _interval;
        private readonly int _retryCount;

        public DoorCheckService(IPingService pingService, IConfiguration configuration)
        {
            _pingService = pingService ?? throw new ArgumentNullException(nameof(pingService));
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if(!TimeSpan.TryParse(configuration["Retry:Interval"], out _interval))
                _interval = TimeSpan.FromSeconds(2);
            if(!Int32.TryParse(configuration["Retry:Count"], out _retryCount))
                _retryCount = 2;
        }

        /// <summary>
        /// Gets status of door
        /// </summary>
        /// <param name="door">Door you want to check status</param>
        /// <returns>if door is online</returns>
        public async Task<bool> GetDoorStatus(Door door)
        {

            if (!IPAddress.TryParse(door.IPAddress, out var ipAddress))
                throw new ArgumentException("Door IP address is not a valid address");

            return await _pingService.SendWithRetry(ipAddress,_retryCount,_interval);
        }

    }
}