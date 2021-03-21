using AutoMapper;
using ParkBee.Assessment.Application.Garages;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.Domain.Models;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapDoorToDoorDto()
        {
            var entity = new Door();

            var result = _mapper.Map<DoorDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<DoorDto>(result);
        }

        [Fact]
        public void ShouldMapGarageToGarageDto()
        {
            var entity = new Garage();

            var result = _mapper.Map<GarageDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<GarageDto>(result);
        }
        
    }
}
