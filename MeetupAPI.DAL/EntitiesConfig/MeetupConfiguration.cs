using MeetupAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.EntitiesConfig
{
    public class MeetupConfiguration : IEntityTypeConfiguration<Meetup>
    {
        public void Configure(EntityTypeBuilder<Meetup> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("TEXT");

            builder.Property(e => e.OrganizerId)
                .IsRequired();

            builder.Property(e => e.Location)
                .HasMaxLength(100);

            builder.Property(e => e.DateAndTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
