using System.Collections.Generic;

namespace ParkBee.Assessment.Application.Garages.Contracts
{
    public class GarageDto
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<DoorDto> Doors { get; set; }
    }
}
