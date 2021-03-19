using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Interfaces
{
    public partial interface IApplicationDbContext
    {
        DbSet<Door> Doors { get; set; }
        DbSet<DoorStatus> DoorStatuses { get; set; }
        DbSet<Garage> Garages { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken= default(CancellationToken));
    }
}