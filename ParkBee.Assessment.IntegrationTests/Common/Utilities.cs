using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParkBee.Assessment.Domain.Models;
using ParkBee.Assessment.Infra.Persistence;

namespace ParkBee.Assessment.IntegrationTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(ApplicationDbContext context)
        {
            context.Garages.RemoveRange(context.Garages);
            context.Doors.RemoveRange(context.Doors);
            context.DoorStatuses.RemoveRange(context.DoorStatuses);
            context.SaveChanges();
            context.Garages.AddRangeAsync(new List<Garage>
            {
                new()
                {
                    GarageId = 1, Name = "Amsterdam",
                    Address = "Train station"
                },
                new()
                {
                    GarageId = 2, Name = "Berlin",
                    Address = "Airport"
                },
            });
            context.Doors.AddRangeAsync(new List<Door>
            {
                new()
                {
                    DoorId = 1, GarageId = 1, Name = "Front Door", IPAddress = "127.0.0.1"
                },
                new()
                {
                    DoorId = 2, GarageId = 2, Name = "Front Door", IPAddress = "8.8.8.8"
                },
                new()
                {
                    DoorId = 3, GarageId = 2, Name = "Back Door", IPAddress = "192.168.100.100"
                }
            });
            context.DoorStatuses.AddRangeAsync(new List<DoorStatus>
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

            context.SaveChanges();
        }
    }
}
