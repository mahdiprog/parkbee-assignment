using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Infra.Configurations
{
    public class DoorStatusConfig : IEntityTypeConfiguration<DoorStatus>
    {
        public void Configure(EntityTypeBuilder<DoorStatus> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.DoorStatusId);
            builder.Property(e => e.DoorStatusId).ValueGeneratedOnAdd();


            builder.HasOne(d => d.Door)
                .WithMany(p => p.DoorStatuses)
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
