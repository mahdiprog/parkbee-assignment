using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Services
{
    public class DoorCheckService : IDoorCheckService
    {
        private readonly IPingService _pingService;

        public DoorCheckService(IPingService pingService)
        {
            _pingService = pingService ?? throw new ArgumentNullException(nameof(pingService));
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

            return await _pingService.SendWithRetry(ipAddress);
        }

    }
}