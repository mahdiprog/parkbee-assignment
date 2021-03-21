using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ParkBee.Assessment.Application.Garages;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.Application.Garages.Queries;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Infra.Persistence;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Garages
{
    public class GetGarageDetailsQueryTests:IDisposable
    {
        private const int GarageId = 1;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
          private readonly Mock<ICurrentUserService> _currentUserServiceMock;

        public GetGarageDetailsQueryTests()
        {
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.GarageId).Returns(GarageId);
            _dbContext = ApplicationDbContextFactory.Create(_currentUserServiceMock.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(_dbContext);
        }

        [Fact]
        public async Task GetGarageDetails()
        {
            var sut = new GetGarageDetailsQueryHandler(_dbContext, _mapper,_currentUserServiceMock.Object);

            var result = await sut.Handle(new GetGarageDetailsQuery(), CancellationToken.None);
            Assert.Equal(GarageId,result.GarageId);
            Assert.Equal(2,result.Doors.Count());
            Assert.IsType<GarageDto>(result);

        }
    }
}
