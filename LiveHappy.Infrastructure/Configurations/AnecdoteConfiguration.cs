using LiveHappy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Infrastructure.Configurations
{
    public class AnecdoteConfiguration : IEntityTypeConfiguration<Anecdote>
    {
        public void Configure(EntityTypeBuilder<Anecdote> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.Title);

            builder.Property(a => a.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("'1900-01-01T00:00:00.000'");

            builder.Ignore(a => a.ShortContent);
        }
    }
}
