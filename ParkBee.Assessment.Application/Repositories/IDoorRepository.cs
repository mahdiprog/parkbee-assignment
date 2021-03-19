using System.Collections.Generic;
using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Repositories
{
    public interface IDoorRepository
    {
        Task ChangeDoorStatus(int doorId, bool isOnline);
        Task<IReadOnlyList<Door>> GetDoors();
        Task<Door> GetDoorWithLatestStatus(int doorId);
    }
}