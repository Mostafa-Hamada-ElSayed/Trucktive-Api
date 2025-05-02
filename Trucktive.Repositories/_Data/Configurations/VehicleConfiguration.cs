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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {

            builder.Property(v => v.Brand)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(v => v.Model)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(v => v.Year)
                   .IsRequired();

            builder.HasOne(v => v.Company)
                   .WithMany(c => c.Vehicles)
                   .HasForeignKey(v => v.CompanyId);

            builder.HasOne(v => v.Supervisor)
                   .WithMany(s => s.Vehicles)
                   .HasForeignKey(v => v.SupervisorId);

            builder.HasMany(v => v.Trips)
                   .WithOne(t => t.Vehicle)
                   .HasForeignKey(t => t.VehicleId);

            builder.HasMany(v => v.Faults)
                   .WithOne(f => f.Vehicle)
                   .HasForeignKey(f => f.VehicleId);
        }
    }
}
