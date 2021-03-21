using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Garages
{
    public class RefreshDoorStatusCommand:IRequest<bool>
    {
        public int DoorId { get; set; }
    }
    public class RefreshDoorStatusCommandHandler : IRequestHandler<RefreshDoorStatusCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDoorCheckService _doorCheckService;

        public RefreshDoorStatusCommandHandler( IApplicationDbContext dbContext, IDoorCheckService doorCheckService)
        {
            _dbContext = dbContext;
            _doorCheckService = doorCheckService;
        }

        public async Task<bool> Handle(RefreshDoorStatusCommand request, CancellationToken cancellationToken)
        {
            var door = await _dbContext.DoorRepository.GetDoorWithLatestStatus(request.DoorId);
            if (door == null)
                throw new PrimaryKeyNotFoundException($"Door with Id {request.DoorId} not found");

            var isOnline = await _doorCheckService.GetDoorStatus(door);
            await _dbContext.DoorRepository.ChangeDoorStatus(door.DoorId, isOnline);
            return isOnline;
        }
    }
}
