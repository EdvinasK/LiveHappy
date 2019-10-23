using LiveHappy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("'1900-01-01T00:00:00.000'");

            builder.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasDefaultValueSql("'1900-01-01T00:00:00.000'");
        }
    }
}
