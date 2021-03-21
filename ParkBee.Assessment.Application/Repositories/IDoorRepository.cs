using System.Collections.Generic;
using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Repositories
{
    public interface IDoorRepository
    {
        Task ChangeDoorStatus(int doorId, bool isOnline);
        Task<IReadOnlyList<Door>> GetAllDoors();
        Task<Door> GetDoorWithLatestStatus(int doorId);
        Task ChangeDoorStatus(Door door, bool isOnline);
    }
}