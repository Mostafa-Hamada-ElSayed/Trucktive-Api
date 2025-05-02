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
    public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
    {
        public void Configure(EntityTypeBuilder<Supervisor> builder)
        {

            builder.Property(s => s.FName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.LName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(s => s.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasOne(s => s.Company)
                .WithMany(c => c.Supervisors)
                .HasForeignKey(s => s.CompanyId);

            builder.HasMany(s => s.Drivers)
                .WithOne(d => d.Supervisor)
                .HasForeignKey(d => d.SupervisorId);

            builder.HasMany(s => s.Vehicles)
                .WithOne(v => v.Supervisor)
                .HasForeignKey(v => v.SupervisorId);
        }
    }
}
