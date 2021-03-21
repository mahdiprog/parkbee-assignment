using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkBee.Assessment.Application.Garages;
using ParkBee.Assessment.Application.Garages.Commands;
using ParkBee.Assessment.Application.Garages.Contracts;
using ParkBee.Assessment.Application.Garages.Queries;

namespace ParkBee.Assessment.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GarageController:BaseController
    {
        /// <summary>
        /// Get the Garage that authenticated user have access
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="400">If can't find a Garage that you have access to</response>  
        [ProducesResponseType(typeof(GarageDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<GarageDto> GetMyGarageDetails()
        {
            return await Mediator.Send(new GetGarageDetailsQuery()).ConfigureAwait(false);
        }
        /// <summary>
        /// refreshes door status and return it
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns>true if it's online and false if it's offline</returns>
        /// <response code="400">If can't find a Door with provided doorId that you have access to</response>  
        [ProducesResponseType(typeof(bool),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Refresh/{doorId:int}")]
        public async Task<bool> RefreshDoorStatus(int doorId)
        {
            return await Mediator.Send(new RefreshDoorStatusCommand{DoorId = doorId}).ConfigureAwait(false);
        }
    }
}