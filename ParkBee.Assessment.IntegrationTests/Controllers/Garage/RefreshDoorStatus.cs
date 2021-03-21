using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ParkBee.Assessment.API;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.IntegrationTests.Common;
using Xunit;

namespace ParkBee.Assessment.IntegrationTests.Controllers.Garage
{
    public class RefreshDoorStatus : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public RefreshDoorStatus(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsBoolean()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            
            var response = await client.PostAsync($"/api/garage/refresh/2",new StringContent(""));

            response.EnsureSuccessStatusCode();
            var vm = await Utilities.GetResponseContent<bool>(response);

            Assert.IsType<bool>(vm);
        }
        
    }
}
