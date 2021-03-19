using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Models;
using ReservationService.Application.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Interfaces
{
    public partial interface IApplicationDbContext
    {
        IDoorRepository DoorRepository { get; }
    }
}