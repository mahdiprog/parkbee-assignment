using ParkBee.Assessment.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationService.Application.Repositories
{
    public interface IDoorRepository
    {
        Task ChangeDoorStatus(int doorId, bool isOnline);
        Task<IReadOnlyList<Door>> GetDoors();
        Task<Door> GetDoorWithLatestStatus(int doorId);
    }
}