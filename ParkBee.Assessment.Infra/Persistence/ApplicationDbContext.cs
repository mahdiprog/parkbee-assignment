using Microsoft.EntityFrameworkCore;
using ParkBee.Assessment.Application.Interfaces;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Infra.Persistence
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }


        public DbSet<Garage> Garages { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorStatus> DoorStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            // Seed data
            modelBuilder.Seed();
        }
    }


}