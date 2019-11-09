using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LiveHappy.Domain.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsUserAdmin(this ClaimsPrincipal source)
        {
            return source.IsInRole("Admin");
        }
    }
}
