using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.CryptoCurrencies;
using CryptoRate.Application.Currencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using CryptoRate.Domain.Models;
using Moq;
using Xunit;

namespace CryptoRate.Application.UnitTests.Currencies
{
    
    [Collection("QueryCollection")]
    public class GetExchangeRatesTests
    { 
        
        [Fact]
        public async Task GetExchangeRates()
        {
            var exchangeRateServiceMock = new Mock<IExchangeRateService>();
            exchangeRateServiceMock.Setup(x => x.GetLatestRate(It.IsAny<string>(),It.IsAny<string>()))
                .ReturnsAsync<string,string,IExchangeRateService,ExchangeRate>((s,b)=> new ExchangeRate{Base = b, Rates = new Dictionary<string, decimal> {{s, (decimal) 1.1}}}); 
            
            var sut = new GetExchangeRatesQueryHandler(exchangeRateServiceMock.Object);

            var result = await sut.Handle(new GetExchangeRatesQuery{symbols = new []{"Test"}} , CancellationToken.None);

            Assert.IsAssignableFrom<ExchangeRate>(result);
            Assert.Contains(result.Rates, i =>i.Key=="Test"&& i.Value==(decimal)1.1);
        }
    }
}
