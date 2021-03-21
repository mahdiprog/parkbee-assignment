using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Repositories
{
    public class DoorRepository : IDoorRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DoorRepository(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<IReadOnlyList<Door>> GetAllDoors()
        {
            return await _dbContext.Doors.ToListAsync();
        }

        public async Task<Door> GetDoorWithLatestStatus(int doorId)
        {
            return await _dbContext.Doors.Include(d => d.DoorStatuses.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(d => d.DoorId == doorId && d.Garage.GarageId == _currentUserService.GarageId);
        }

        public async Task ChangeDoorStatus(int doorId, bool isOnline)
        {
            var door = await GetDoorWithLatestStatus(doorId);
            if (door != null)
                await ChangeDoorStatus(door, isOnline);
        }

        public async Task ChangeDoorStatus(Door door, bool isOnline)
        {
            if (!door.DoorStatuses.Any() || door.DoorStatuses.First().IsOnline != isOnline)
                door.DoorStatuses.Add(new DoorStatus {IsOnline = isOnline, ChangeDate = DateTimeOffset.Now});

            await _dbContext.SaveChangesAsync();
        }
    }
}
