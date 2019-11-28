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
    public class EventUserConfiguration : IEntityTypeConfiguration<EventUser>
    {
        public void Configure(EntityTypeBuilder<EventUser> builder)
        {
            builder.HasKey(c => new { c.EventId, c.UserId });

            builder.Property(cb => cb.EventValue)
                .IsRequired();

            builder.Property(c => c.DrinksValue)
                .IsRequired();
        }
    }
}