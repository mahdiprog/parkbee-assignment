using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ParkBee.Assessment.Application.Garages;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Garage, GarageDto>();
            CreateMap<Door, DoorDto>().MaxDepth(0).ForMember(d=>d.IsOnline,c=>c.MapFrom(s=>s.DoorStatuses.FirstOrDefault().IsOnline));
        }
    }
}
