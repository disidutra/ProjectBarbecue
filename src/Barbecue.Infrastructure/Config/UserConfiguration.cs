using Barbecue.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbecue.Infra.Data.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c=> c.Email)
                .IsUnique();

            builder.Property(c => c.Id)
               .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(254);

            builder.Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}