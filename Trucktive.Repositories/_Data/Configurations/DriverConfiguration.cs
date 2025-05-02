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
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {

            builder.Property(d => d.FName).IsRequired().HasMaxLength(100);

            builder.Property(d => d.LName).IsRequired().HasMaxLength(100);

            builder.Property(d => d.Email).IsRequired().HasMaxLength(100);

            builder.Property(d => d.Phone).IsRequired().HasMaxLength(20);

            builder.Property(d => d.Address).HasMaxLength(200);

            builder.Property(d => d.Rate).HasColumnType("decimal(18,2)");

            builder.HasOne(d => d.Supervisor)
                   .WithMany(s => s.Drivers)
                   .HasForeignKey(d => d.SupervisorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Trips)
                   .WithOne(t => t.Driver)
                   .HasForeignKey(t => t.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Behaviors)
                   .WithOne(b => b.Driver)
                   .HasForeignKey(b => b.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
