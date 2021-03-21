using System;
using System.Net;
using System.Threading.Tasks;
using Moq;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Services;
using ParkBee.Assessment.Domain.Models;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Services
{
    public class DoorCheckServiceTests
    {
        private const string BlockedIpString = "0.0.0.0";
        private const string AliveIpString = "1.1.1.1";
        private readonly DoorCheckService _doorCheckService;
        public DoorCheckServiceTests()
        {
            var pingServiceMock = new Mock<IPingService>();
            pingServiceMock.Setup(x => x.SendWithRetry(IPAddress.Parse(BlockedIpString))).ReturnsAsync(false);
            pingServiceMock.Setup(x => x.SendWithRetry(IPAddress.Parse(AliveIpString))).ReturnsAsync(true);
            _doorCheckService = new DoorCheckService(pingServiceMock.Object);
        }

        [Fact]
        public void ShouldCheckForRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new DoorCheckService(null));
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
