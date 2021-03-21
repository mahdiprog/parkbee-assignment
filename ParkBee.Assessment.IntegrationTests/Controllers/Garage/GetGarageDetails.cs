using System.Threading.Tasks;
using ParkBee.Assessment.API;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.IntegrationTests.Common;
using Xunit;

namespace ParkBee.Assessment.IntegrationTests.Controllers.Garage
{
    public class GetGarageDetails : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetGarageDetails(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsGarage()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            
            var response = await client.GetAsync($"/api/garage");

            response.EnsureSuccessStatusCode();
            var vm = await Utilities.GetResponseContent<GarageDto>(response);

            Assert.IsType<GarageDto>(vm);
            Assert.Equal(2,vm.GarageId);
        }
    }
}
