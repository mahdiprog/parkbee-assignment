
namespace ParkBee.Assessment.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string Name { get; }
        int GarageId { get; }
        bool IsAuthenticated { get; }
    }
}
