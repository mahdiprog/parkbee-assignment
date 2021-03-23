using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Repositories;
using ParkBee.Assessment.Domain.Models;
using ParkBee.Assessment.Infra.Persistence;

namespace ParkBee.Assessment.Application.UnitTests.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create(ICurrentUserContext currentUserService)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options, currentUserService);

            context.Database.EnsureCreated();
            context.Garages.RemoveRange(context.Garages);
            context.Doors.RemoveRange(context.Doors);
            context.DoorStatuses.RemoveRange(context.DoorStatuses);
            context.SaveChanges();

            context.Garages.AddRange(new Garage {GarageId = 1}, new Garage {GarageId = 2});
            context.SaveChanges();
            context.Doors.AddRange(
                new Door
                {
                    GarageId = 1, DoorId = 1
                },
                new Door
                {
                    GarageId = 1, DoorId = 2
                },
                new Door
                {
                    GarageId = 2, DoorId = 3
                });

            context.SaveChanges();
            context.DoorStatuses.AddRange(new DoorStatus {DoorId = 1, DoorStatusId = 1, IsOnline = true, ChangeDate = DateTimeOffset.Now},
                new DoorStatus {DoorId = 1, DoorStatusId = 2, IsOnline = false, ChangeDate = DateTimeOffset.Now.AddDays(-1)},
                new DoorStatus {DoorId = 2, DoorStatusId = 3, IsOnline = false},
                new DoorStatus {DoorId = 3, DoorStatusId = 4, IsOnline = false});
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}