using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Garages.Commands;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Domain.Models;
using ParkBee.Assessment.Infra.Persistence;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Garages
{
    public class RefreshDoorStatusCommandTests:IDisposable{

        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IDoorCheckService> _doorCheckServiceMock;
        private readonly Mock<ICurrentUserContext> _currentUserContextMock;

        public RefreshDoorStatusCommandTests()
        {
            _currentUserContextMock = new Mock<ICurrentUserContext>();
            _currentUserContextMock.Setup(m => m.GarageId).Returns(1);
            _dbContext = ApplicationDbContextFactory.Create();

            _doorCheckServiceMock = new Mock<IDoorCheckService>(); 
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_dbContext);
        }
        [Fact]
        public void ShouldCheckForRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(_dbContext, _doorCheckServiceMock.Object, null));
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(null, _doorCheckServiceMock.Object, _currentUserContextMock.Object));
            Assert.Throws<ArgumentNullException>(() => new RefreshDoorStatusCommandHandler(_dbContext, _doorCheckServiceMock.Object,null));
        }
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldReturnDoorStatus()
        {
            // Arrange
           _doorCheckServiceMock.Setup(cs => cs.GetDoorStatus(It.Is<Door>(d => d.DoorId == 1))).ReturnsAsync(false);
            _doorCheckServiceMock.Setup(cs => cs.GetDoorStatus(It.Is<Door>(d => d.DoorId == 2))).ReturnsAsync(true);
            var sut = new RefreshDoorStatusCommandHandler(_dbContext, _doorCheckServiceMock.Object,_currentUserContextMock.Object);

            // Act
            // check if throws exception on door not exists
            await Assert.ThrowsAsync<NotFoundException>(()=>sut.Handle(new RefreshDoorStatusCommand { DoorId = 3 }, CancellationToken.None));

            // check if it returns correct status
            Assert.False(await sut.Handle(new RefreshDoorStatusCommand { DoorId = 1 }, CancellationToken.None));
            Assert.True(await sut.Handle(new RefreshDoorStatusCommand { DoorId = 2 }, CancellationToken.None));
        }
    }
}
