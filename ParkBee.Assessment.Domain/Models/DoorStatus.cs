using System;
using System.Collections.Generic;
using System.Text;

namespace ParkBee.Assessment.Domain.Models
{
    public class DoorStatus
    {
        public int DoorStatusId { get; set; }
        public int DoorId { get; set; }
        public bool IsOnline{ get; set; }
        public DateTimeOffset ChangeDate { get; set; }
        public Door Door { get; set; }

    }
}
