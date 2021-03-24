using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Services.CronJobs
{
    public class GetDoorsStatusesCronJob : CronJobService
    {
        private readonly ILogger<GetDoorsStatusesCronJob> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetDoorsStatusesCronJob(IScheduleConfig<GetDoorsStatusesCronJob> config,
            ILogger<GetDoorsStatusesCronJob> logger, IServiceScopeFactory serviceScopeFactory)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDoorsStatusesCronJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<IApplicationDbContext>();
            var doorCheckService = scope.ServiceProvider.GetService<IDoorCheckService>();
            var doors = await dbContext.DoorRepository.GetAllDoors();
            foreach (var door in doors)
            {
                var isOnline = await doorCheckService.GetDoorStatus(door);
                await dbContext.DoorRepository.ChangeDoorStatus(door, isOnline);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDoorsStatusesCronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}