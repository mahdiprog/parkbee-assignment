using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Exceptions;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Garages.Queries
{
    public class GetGarageDetailsQuery:IRequest<GarageDto>
    {
       
    }
    public class GetGarageDetailsQueryHandler : IRequestHandler<GetGarageDetailsQuery, GarageDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetGarageDetailsQueryHandler( IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<GarageDto> Handle(GetGarageDetailsQuery request, CancellationToken cancellationToken)
        {
            // get the garage which current user is it's owner
            var garage = await _dbContext.Garages.Include(g => g.Doors)
                .ThenInclude(d => d.DoorStatuses.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(g => g.GarageId == _currentUserService.GarageId, cancellationToken: cancellationToken);
            if (garage == null)
                throw new NotFoundException($"Garage with Id {_currentUserService.GarageId} not found");

            return _mapper.Map<GarageDto>(garage);
        }
    }
}
