using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkBee.Assessment.Application.Garages;

namespace ParkBee.Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GarageController:BaseController
    {
        [HttpGet]
        public async Task<GarageDto> GetMyGarageDetails()
        {
            return await Mediator.Send(new GetGarageDetailsQuery()).ConfigureAwait(false);
        }

        [HttpPost("Refresh/{doorId:int}")]
        public async Task<bool> RefreshDoorStatus(int doorId)
        {
            return await Mediator.Send(new RefreshDoorStatusCommand{DoorId = doorId}).ConfigureAwait(false);
        }
    }
}