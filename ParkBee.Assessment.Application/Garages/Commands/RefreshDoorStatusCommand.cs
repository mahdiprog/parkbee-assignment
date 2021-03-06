using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Garages.Commands
{
    public class RefreshDoorStatusCommand:IRequest<bool>
    {
        public int DoorId { get; set; }
    }
    public class RefreshDoorStatusCommandHandler : IRequestHandler<RefreshDoorStatusCommand, bool>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IDoorCheckService _doorCheckService;
        private readonly ICurrentUserContext _currentUserContext;

        public RefreshDoorStatusCommandHandler( IApplicationDbContext dbContext, IDoorCheckService doorCheckService, ICurrentUserContext currentUserContext)
        {
            _dbContext = dbContext?? throw new ArgumentNullException(nameof(dbContext));
            _doorCheckService = doorCheckService?? throw new ArgumentNullException(nameof(doorCheckService));
            _currentUserContext = currentUserContext?? throw new ArgumentNullException(nameof(currentUserContext));
        }

        public async Task<bool> Handle(RefreshDoorStatusCommand request, CancellationToken cancellationToken)
        {
            // get door with latest status included in
            var door = await _dbContext.DoorRepository.GetDoorWithLatestStatus(request.DoorId, _currentUserContext.GarageId);
            if (door == null)
                throw new NotFoundException($"Door with Id {request.DoorId} not found");
            // check if door is online
            var isOnline = await _doorCheckService.GetDoorStatus(door);
            await _dbContext.DoorRepository.ChangeDoorStatus(door, isOnline);
            return isOnline;
        }
    }
}
