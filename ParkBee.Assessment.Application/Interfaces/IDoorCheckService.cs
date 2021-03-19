using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Services
{
    public interface IDoorCheckService
    {
        Task<bool> GetDoorStatus(int doorId);
    }
}