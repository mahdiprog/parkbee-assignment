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
        private readonly ICurrentUserContext _currentUserContext;

        public GetGarageDetailsQueryHandler( IApplicationDbContext dbContext, IMapper mapper, ICurrentUserContext currentUserContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserContext = currentUserContext;
        }

        public async Task<GarageDto> Handle(GetGarageDetailsQuery request, CancellationToken cancellationToken)
        {
            // get the garage which current user is it's owner
            var garage = await _dbContext.Garages.Include(g => g.Doors)
                .ThenInclude(d => d.DoorStatuses.OrderByDescending(p => p.ChangeDate).Take(1))
                .FirstOrDefaultAsync(g => g.GarageId == _currentUserContext.GarageId, cancellationToken: cancellationToken);
            if (garage == null)
                throw new NotFoundException($"Garage with Id {_currentUserContext.GarageId} not found");

            return _mapper.Map<GarageDto>(garage);
        }
    }
}
