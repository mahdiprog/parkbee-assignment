using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.Currencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.Services;
using CryptoRate.Application.UnitTests.Common;
using CryptoRate.Application.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace CryptoRate.Application.UnitTests.Services
{
    public class ExchangeRateServiceTests
    {
        private readonly Mock<ILogger<ExchangeRateService>> _loggerMock = new Mock<ILogger<ExchangeRateService>>();
        private readonly Mock<IConfiguration>  _configurationMock = new Mock<IConfiguration>();
        private readonly MemoryCache _cache;
            public ExchangeRateServiceTests()
        {
            _configurationMock.SetupGet(x => x[It.Is<string>(s=>s == "ExchangeRateApi:CacheTimeout")]).Returns("00:00:02");
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        
        [Fact]
        public async Task GetLatestRateTests()
        {
            var client = Helpers.GetHttpClient(
                "{\"base\":\"EUR\",\"date\":\"2018-04-08\",\"rates\":{\"CAD\":1.565,\"CHF\":1.1798,\"GBP\":0.87295,\"SEK\":10.2983,\"EUR\":1.092,\"USD\":1.2234}}");
            var sut = new ExchangeRateService(client,_cache,_configurationMock.Object,_loggerMock.Object);
            
            // first run should get data from api mock
            var result = await sut.GetLatestRate("EUR,GBP");

            Assert.IsAssignableFrom<ExchangeRate>(result);
            Assert.Contains(result.Rates, i =>i.Key=="CAD"&& i.Value==(decimal)1.565);
            
            // second run should get data from cache
            client = Helpers.GetHttpClient("{}");
            sut = new ExchangeRateService(client,_cache,_configurationMock.Object,_loggerMock.Object);
            result = await sut.GetLatestRate("EUR,GBP");
            Assert.IsAssignableFrom<ExchangeRate>(result);
            Assert.Contains(result.Rates, i =>i.Key=="CAD"&& i.Value==(decimal)1.565);
        }
    }
}
