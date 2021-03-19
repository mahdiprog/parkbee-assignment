using System.Threading.Tasks;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application.Interfaces
{
    public interface IDoorCheckService
    {
        Task<bool> GetDoorStatus(Door door);
    }
}