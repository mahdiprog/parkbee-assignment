using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Domain.Models;

namespace Meetings.Infra.Persistence
{
    public static class DatabaseSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
                        modelBuilder.Entity<Garage>().HasData(new List<Garage>
            {
                new()
                {
                    GarageId = 1, Name = "Amsterdam",
                    Address = "Train station"
                },
                new()
                {
                    GarageId = 2, Name = "Berlin",
                    Address = "Train station"
                },
            });
            modelBuilder.Entity<Door>().HasData(new List<Door>
            {
                new()
                {
                    DoorId = 1, GarageId=1, Name = "Front Door", IPAddress="127.0.0.1"
                },
                new()
                {
                    DoorId = 2, GarageId=2, Name = "Front Door", IPAddress="127.0.0.1"
                },
                new()
                {
                    DoorId = 3, GarageId=2, Name = "Back Door", IPAddress="127.0.0.2"
                }
            });
            modelBuilder.Entity<DoorStatus>().HasData(new List<DoorStatus>
            {
                new()
                {
                    DoorStatusId = 1,
                    DoorId = 1, IsOnline = true, ChangeDate = DateTimeOffset.Now.AddDays(-1),

                },
                new()
                {
                                        DoorStatusId = 2,
                    DoorId = 2, IsOnline = false, ChangeDate = DateTimeOffset.Now.AddDays(-1),
                },
                new()
                {
                                                            DoorStatusId = 3,
                    DoorId = 2, IsOnline = true, ChangeDate = DateTimeOffset.Now.AddHours(-12),

                },
                new()
                {
                    DoorStatusId = 4,
                    DoorId = 3, IsOnline = true, ChangeDate = DateTimeOffset.Now.AddHours(-2),
                },
            });
        }
    }
}
