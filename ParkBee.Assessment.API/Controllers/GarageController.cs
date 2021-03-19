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
    }
}