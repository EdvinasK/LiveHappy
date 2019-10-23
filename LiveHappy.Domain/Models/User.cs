using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHappy.Domain.Models
{
    public class User : IdentityUser<string>
    {
        public string Nickname { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
