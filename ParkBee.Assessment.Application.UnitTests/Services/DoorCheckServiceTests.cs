using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Services;
using ParkBee.Assessment.Domain.Models;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Services
{
    public class DoorCheckServiceTests
    {
        private const string BlockedIpString = "1.1.1.1";
        private const string AliveIpString = "0.0.0.0";
        private readonly DoorCheckService _doorCheckService;
        private readonly Mock<IConfiguration>  _configurationMock = new();
        readonly Mock<IPingService> _pingServiceMock= new();
        public DoorCheckServiceTests()
        {
            _configurationMock.SetupGet(x => x[It.Is<string>(s=>s == "Retry:Count")]).Returns("2");

            _pingServiceMock.Setup(x => x.SendWithRetry(IPAddress.Parse(BlockedIpString),2,TimeSpan.FromSeconds(2))).ReturnsAsync(false);
            _pingServiceMock.Setup(x => x.SendWithRetry(IPAddress.Parse(AliveIpString),2,TimeSpan.FromSeconds(2))).ReturnsAsync(true);
            _doorCheckService = new DoorCheckService(_pingServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void ShouldCheckForRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new DoorCheckService(null,_configurationMock.Object));
            Assert.Throws<ArgumentNullException>(() => new DoorCheckService(_pingServiceMock.Object,null));
        }
        [Fact]
        public async Task InvalidIpShouldThrowArgumentException()
        {
            // check if invalid IP throws an exception
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _doorCheckService.GetDoorStatus(new Door { IPAddress = "" }));
            Assert.Equal("Door IP address is not a valid address", ex.Message);
        }

        [Fact]
        public async Task ResultShouldReflectsPingService()
        {
            var result = await _doorCheckService.GetDoorStatus(new Door { IPAddress = BlockedIpString });

            Assert.False(result);

            result = await _doorCheckService.GetDoorStatus(new Door { IPAddress = AliveIpString });

            Assert.True(result);
        }
    }
}
