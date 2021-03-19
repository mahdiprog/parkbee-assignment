using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Assessment.Domain.Models
{
    public class Garage
    {
        public int GarageId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Door> Doors { get; set; }

    }
}
