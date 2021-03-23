
namespace ParkBee.Assessment.Application.Interfaces
{
    public interface ICurrentUserContext
    {
        string Name { get; }
        int GarageId { get; }
        bool IsAuthenticated { get; }
    }
}
