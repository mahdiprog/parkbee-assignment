using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkBee.Assessment.Domain.Models;

namespace ParkBee.Assessment.Infra.Configurations
{
    public class GarageConfig : IEntityTypeConfiguration<Garage>
    {
        public void Configure(EntityTypeBuilder<Garage> builder)
        {

            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.HasKey(e => e.GarageId);
            builder.Property(e => e.GarageId).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasMaxLength(64);
            builder.Property(e => e.Address).HasMaxLength(512);

        }
    }
}
