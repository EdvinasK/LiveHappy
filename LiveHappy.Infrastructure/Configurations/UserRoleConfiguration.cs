using LiveHappy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHappy.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole { Id = "b154ed34-4353-49e5-bd7a-4c5786695d7b", Name = "Admin", NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = "69fde719-7e1c-4f2b-83b3-35987adfecb6", Description = "Administrator user" },
                new UserRole { Id = "0736d402-175c-4d36-ad73-cb1c1537fdb0", Name = "VIP", NormalizedName = "VIP".ToUpper(), ConcurrencyStamp = "a2899d94-6965-40c3-8fac-374f92ff5fcc", Description = "User with additional features" },
                new UserRole { Id = "f2f5d08f-075e-4ff9-8bda-d385ebad0550", Name = "Member", NormalizedName = "Member".ToUpper(), ConcurrencyStamp = "80487ed3-d8bf-423e-a52f-0de56d5e47fe", Description = "Basic user" }
            );
        }
    }
}
