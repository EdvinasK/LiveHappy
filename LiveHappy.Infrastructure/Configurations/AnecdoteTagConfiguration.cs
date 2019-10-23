using LiveHappy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Infrastructure.Configurations
{
    public class AnecdoteTagConfiguration : IEntityTypeConfiguration<AnecdoteTag>
    {
        public void Configure(EntityTypeBuilder<AnecdoteTag> builder)
        {
            builder.HasKey(at => new { at.AnecdoteId, at.TagId });

            builder.HasOne(at => at.Anecdote)
                .WithMany(a => a.AnecdoteTags)
                .HasForeignKey(at => at.AnecdoteId);

            builder.HasOne(at => at.Tag)
                .WithMany(t => t.AnecdoteTags)
                .HasForeignKey(at => at.TagId);
        }
    }
}
