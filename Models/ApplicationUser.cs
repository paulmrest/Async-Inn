using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Async_Inn.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public static class ApplicationRoles
    {
        public const string DistrictManager = "District Manager";
        public const string PropertyManager = "Property Manager";
        public const string Agent = "Agent";
    }
}
