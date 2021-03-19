using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkBee.Assessment.Application.Repositories;

namespace ParkBee.Assessment.Infra.Persistence
{
    public partial class ApplicationDbContext 
    {
        private IDoorRepository _doorRepository;
        public IDoorRepository DoorRepository => _doorRepository ??= new DoorRepository(this);
    }


}