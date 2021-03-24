using ParkBee.Assessment.Application.Repositories;

namespace ParkBee.Assessment.Infra.Persistence
{
    public partial class ApplicationDbContext 
    {
        private IDoorRepository _doorRepository;
        public IDoorRepository DoorRepository => _doorRepository ??= new DoorRepository(this);
    }


}