using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Application.Services;
using ParkBee.Assessment.Application.Services.CronJobs;

namespace ParkBee.Assessment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IDoorCheckService, DoorCheckService>();
            // Add Cron jobs
            services.AddCronJob<GetDoorsStatusesCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = configuration["CronJobExpression"];
            });

            return services;
        }
    }
}
