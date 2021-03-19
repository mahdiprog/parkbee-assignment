using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Services
{
    public class DoorCheckService : IDoorCheckService
    {
        private readonly IPingService _pingService;
        private readonly ILogger<DoorCheckService> _logger;

        public DoorCheckService(IPingService pingService, IConfiguration configuration,
            ILogger<DoorCheckService> logger)
        {
            _pingService = pingService;
            _logger = logger;
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