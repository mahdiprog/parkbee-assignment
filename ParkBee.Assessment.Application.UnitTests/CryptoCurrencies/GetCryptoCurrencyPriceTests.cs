using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.CryptoCurrencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Domain.Models;
using Moq;
using Xunit;

namespace CryptoRate.Application.UnitTests.CryptoCurrencies
{
   
    [Collection("QueryCollection")]
    public class GetCryptoCurrencyPriceTests
    { 
        [Fact]
        public async Task GetAllCryptoCurrencies()
        {
            var cryptoCurrencyServiceMock = new Mock<ICryptoCurrencyService>();
            cryptoCurrencyServiceMock.Setup(x => x.GetLatestPrice("Test")).ReturnsAsync((decimal)1.1);
            var sut = new GetCryptoCurrencyPriceQueryHandler(cryptoCurrencyServiceMock.Object);

            var result = await sut.Handle(new GetCryptoCurrencyPriceQuery{Symbol = "Test"}, CancellationToken.None);

            Assert.Equal((decimal)1.1, result);
        }
    }
}
