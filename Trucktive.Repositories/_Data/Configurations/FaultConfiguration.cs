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
    public class FaultConfiguration : IEntityTypeConfiguration<Fault>
    {
        public void Configure(EntityTypeBuilder<Fault> builder)
        {
            builder.Property(f => f.ProblemName)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(f => f.Time)
                   .IsRequired();

            builder.Property(f => f.SecurityLevel)
                   .IsRequired();

            builder.HasOne(f => f.Vehicle)
                   .WithMany(v => v.Faults)
                   .HasForeignKey(f => f.VehicleId);
        }
    }
}
