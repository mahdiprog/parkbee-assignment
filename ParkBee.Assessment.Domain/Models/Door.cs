using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Assessment.Domain.Models
{
    public class Door
    {
        public int DoorId { get; set; }
       public int GarageId { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public Garage Garage { get; set; }
        public ICollection<DoorStatus> DoorStatuses { get; set; }
    }
}
