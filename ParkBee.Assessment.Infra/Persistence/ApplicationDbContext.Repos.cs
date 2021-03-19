using Meetings.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;
using ReservationService.Application.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Infra.Persistence
{
    public partial class ApplicationDbContext 
    {
        private IDoorRepository _DoorRepository;
        public IDoorRepository DoorRepository => _DoorRepository ??= new DoorRepository(this);
    }


}