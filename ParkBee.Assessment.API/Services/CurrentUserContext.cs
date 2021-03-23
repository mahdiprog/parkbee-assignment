using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.API.Services
{
    public class CurrentUserContext : ICurrentUserContext
    {
        public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
        {
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            GarageId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ParkBeeClaimTypes.GarageId));
            IsAuthenticated = Name != null;
        }

        public string Name { get; }
        public int GarageId { get; }

        public bool IsAuthenticated { get; }
    }
}
