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
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {

            builder.Property(a => a.FName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.LName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(a => a.Companies)
                .WithOne(c => c.Admin)
                .HasForeignKey(c => c.AdminId);
        }
    }
}
