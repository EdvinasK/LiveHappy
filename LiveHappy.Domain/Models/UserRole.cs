using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Domain.Models
{
    public class UserRole : IdentityRole<string>
    {
        public UserRole() { }
        public UserRole(string roleName) : base(roleName) { }

        public string Description { get; set; }
    }
}
