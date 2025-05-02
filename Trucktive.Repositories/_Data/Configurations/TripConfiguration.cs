using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Entities;

namespace Trucktive.Repositories._Data.Configurations
{
    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {

            builder.Property(t => t.DateTime)
                   .IsRequired();

            builder.Property(t => t.Rate)
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.HashBraking)
                   .IsRequired();

            builder.Property(t => t.HashAcceleration)
                   .IsRequired();

            builder.Property(t => t.IdPrime)
                   .HasColumnType("decimal(18,2)");

            builder.Property(t => t.OverspeedEvents)
                   .IsRequired();

            builder.HasOne(t => t.Vehicle)
                   .WithMany(v => v.Trips)
                   .HasForeignKey(t => t.VehicleId);

            builder.HasOne(t => t.Driver)
                   .WithMany(d => d.Trips)
                   .HasForeignKey(t => t.DriverId);
        }
    }
}
