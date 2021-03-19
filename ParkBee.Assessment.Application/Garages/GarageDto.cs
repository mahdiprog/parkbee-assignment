using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Assessment.Application.Garages
{
    public class GarageDto
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<DoorDto> Doors { get; set; }
    }
}
