using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.CryptoCurrencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Domain.Interfaces;
using CryptoRate.Domain.Models;
using Moq;
using Xunit;

namespace CryptoRate.Application.UnitTests.CryptoCurrencies
{
    
    [Collection("QueryCollection")]
    public class GetExchangeRatesTests
    { 
        

        [Fact]
        public async Task GetAllCryptoCurrencies()
        {
            var cryptoCurrencyServiceMock = new Mock<ICryptoCurrencyService>();
            cryptoCurrencyServiceMock.Setup(x => x.GetAllCurrencies()).ReturnsAsync(new List<CryptoCurrency>{new CryptoCurrency()});
            
            var sut = new GetAllCryptoCurrenciesQueryHandler(cryptoCurrencyServiceMock.Object);

            var result = await sut.Handle(new GetAllCryptoCurrenciesQuery() , CancellationToken.None);

            Assert.IsAssignableFrom<IEnumerable<CryptoCurrency>>(result);
            Assert.Single(result);
        }
    }
}
