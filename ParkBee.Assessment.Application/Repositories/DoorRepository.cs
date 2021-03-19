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

        public DoorRepository(IApplicationDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Door>> GetDoors()
        {
            return await _dbContext.Doors.ToListAsync();
        }
        public async Task<Door> GetDoorWithLatestStatus(int doorId)
        {
            return await _dbContext.Doors.Include(d => d.DoorStatuses.OrderByDescending(p => p.ChangeDate).Take(1)).FirstOrDefaultAsync(d => d.DoorId == doorId);
        }

        public async Task ChangeDoorStatus(int doorId, bool isOnline)
        {
            var door = await GetDoorWithLatestStatus(doorId);

            if (!door.DoorStatuses.Any() || door.DoorStatuses.First().IsOnline != isOnline)
                door.DoorStatuses.Add(new ParkBee.Assessment.Domain.Models.DoorStatus { IsOnline = isOnline, ChangeDate = DateTimeOffset.Now });

            await _dbContext.SaveChangesAsync();
        }
    }
}
