using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Domain.Models
{
    public class UserRole : IdentityRole<string>
    {
        public UserRole() { }

        public string Description { get; set; }
    }
}
