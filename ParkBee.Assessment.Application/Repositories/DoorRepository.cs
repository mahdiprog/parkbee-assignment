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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        /// <summary>
        /// Get All available doors
        /// </summary>
        /// <returns>List of Doors</returns>
        public async Task<IReadOnlyList<Door>> GetAllDoors()
        {
            return await _dbContext.Doors.ToListAsync();
        }
        /// <summary>
        /// Get th door with specified id and include it's latest status
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns>Door with it's last status</returns>
        public async Task<Door> GetDoorWithLatestStatus(int doorId, int garageId)
        {
            return await _dbContext.Doors.Include(d => d.DoorStatuses.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(d => d.DoorId == doorId && d.Garage.GarageId ==garageId);
        }
        /// <summary>
        /// Set door status
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="isOnline"></param>
        public async Task ChangeDoorStatus(int doorId, int garageId, bool isOnline)
        {
            var door = await GetDoorWithLatestStatus(doorId, garageId);
            if (door != null)
                await ChangeDoorStatus(door, isOnline);
        }
        /// <summary>
        /// Set door status
        /// </summary>
        /// <param name="door"></param>
        /// <param name="isOnline"></param>
        /// <returns></returns>
        public async Task ChangeDoorStatus(Door door, bool isOnline)
        {
            // check if door status changed, new DoorStatus record should be added to DB
            if (!door.DoorStatuses.Any() || door.DoorStatuses.First().IsOnline != isOnline)
                door.DoorStatuses.Add(new DoorStatus {IsOnline = isOnline, ChangeDate = DateTimeOffset.Now});

            await _dbContext.SaveChangesAsync();
        }
    }
}
