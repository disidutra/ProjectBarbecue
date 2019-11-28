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
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Description)
               .IsRequired()
               .HasMaxLength(128);

            builder.Property(cb => cb.Date)
                .IsRequired();

            builder.Property(cb => cb.EventValue)
            .IsRequired();

            builder.Property(cb => cb.DrinksValue)
            .IsRequired();

            builder.Property(c => c.Comments)
                .IsRequired()
                .HasMaxLength(500);

            builder.Ignore(c => c.TotalUsers);
            builder.Ignore(c => c.TotalValue);
            builder.Ignore(c => c.TotalPaid);
        }
    }
}