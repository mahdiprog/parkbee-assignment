using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Repositories;
using ParkBee.Assessment.Application.Services;
using ParkBee.Assessment.Application.UnitTests.Common;
using ParkBee.Assessment.Domain.Models;
using ParkBee.Assessment.Infra.Persistence;
using Xunit;

namespace ParkBee.Assessment.Application.UnitTests.Repositories
{
    public class DoorRepositoryTests
    {
        private const int GarageId = 1;

        private readonly ApplicationDbContext _dbContext;
        private readonly IDoorRepository _repo;


        public DoorRepositoryTests()
        {
            
            _dbContext = ApplicationDbContextFactory.Create();
            _repo= new DoorRepository(_dbContext);
        }

        [Fact]
        public void ShouldCheckForRequiredInjects()
        {
            Assert.Throws<ArgumentNullException>(() => new DoorRepository(null));
        }

        [Fact]
        public async Task GetAllDoors_ShouldReturnValue()
        {
            var result = await _repo.GetAllDoors();
            // check if it returns any values
            Assert.NotEmpty(result);
            Assert.Equal(3,result.Count);
        }
        [Fact]
        public async Task GetDoorWithLatestStatus_ShouldReturnOnlyAccessibleDoors()
        {
           var result = await _repo.GetDoorWithLatestStatus(3,GarageId);
            Assert.Null(result);
        }
        [Fact]
        public async Task GetDoorWithLatestStatus_ShouldReturnLatestStatus()
        {
            var result = await _repo.GetDoorWithLatestStatus(1, GarageId);
            Assert.NotNull(result);
            Assert.True(result.DoorStatuses.First().IsOnline);
        }
    }
}
