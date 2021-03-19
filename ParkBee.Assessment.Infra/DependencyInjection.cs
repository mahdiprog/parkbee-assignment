using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Infra.Persistence;

namespace ParkBee.Assessment.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPingService, PingService>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("parkbee"));
            //options.UseSqlServer(configuration.GetConnectionString("ParkBeeDbContext")));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }

}
