using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ParkBee.Assessment.Application.Interfaces;

namespace ParkBee.Assessment.Application.Services.CronJobs
{
    public class GetDoorsStatusesCronJob : CronJobService
    {
        private readonly ILogger<GetDoorsStatusesCronJob> _logger;
        private readonly IDoorCheckService _doorCheckService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public GetDoorsStatusesCronJob(IScheduleConfig<GetDoorsStatusesCronJob> config,
            ILogger<GetDoorsStatusesCronJob> logger,
            IConfiguration configuration, IDoorCheckService doorCheckService, IApplicationDbContext dbContext)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _configuration = configuration;
            _doorCheckService = doorCheckService;
            _dbContext = dbContext;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDoorsStatusesCronJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            var doors = await _dbContext.GetDoors();
            foreach (var door in doors)
            {
                var isOnline = await _doorCheckService.GetDoorStatus(door.DoorId);
                await _dbContext.ChangeDoorStatus(door.DoorId, isOnline);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDoorsStatusesCronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}