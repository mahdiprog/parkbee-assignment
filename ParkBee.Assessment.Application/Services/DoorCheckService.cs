using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Services
{
    public class DoorCheckService : IDoorCheckService
    {
        private readonly HttpClient _httpClient;
        private readonly TimeSpan _slidingExpiration;
        private readonly ILogger<DoorCheckService> _logger;

        public DoorCheckService(HttpClient httpClient, IConfiguration configuration,
            ILogger<DoorCheckService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _slidingExpiration = TimeSpan.Parse(configuration["CoinMarketCap:CacheTimeout"]);
        }

        /// <summary>
        /// Gets the latest price of specified symbol 
        /// </summary>
        /// <param name="doorId">Id of door</param>
        /// <returns>if door is online</returns>
        public async Task<bool> GetDoorStatus(int doorId)
        {
            return true;
        }

    }
}