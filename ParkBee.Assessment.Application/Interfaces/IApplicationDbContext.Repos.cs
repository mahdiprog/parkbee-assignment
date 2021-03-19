using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ParkBee.Assessment.Application.Repositories;

namespace ParkBee.Assessment.Application.Interfaces
{
    public partial interface IApplicationDbContext
    {
        IDoorRepository DoorRepository { get; }
    }
}