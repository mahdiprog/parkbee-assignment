using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Models;

namespace ReservationService.Infra.Configurations
{
    public class DoorConfig : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.DoorId);
            builder.Property(e => e.DoorId).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(64);
            builder.Property(e => e.IPAddress).HasMaxLength(64);

            builder.HasOne(d => d.Garage)
                .WithMany(p => p.Doors)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
