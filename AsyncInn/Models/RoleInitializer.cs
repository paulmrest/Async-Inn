using Async_Inn.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.DistrictManager, NormalizedName = ApplicationRoles.DistrictManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.PropertyManager, NormalizedName = ApplicationRoles.PropertyManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.Agent, NormalizedName = ApplicationRoles.Agent.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
        };

        /// <summary>
        /// Seeds Role data into AsyncInnDbContext.
        /// </summary>
        /// <param name="serviceProvider">
        /// IServiceProvider: an object that implements IServiceProvider
        /// </param>
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using(var dbContext = new AsyncInnDbContext(serviceProvider.GetRequiredService<DbContextOptions<AsyncInnDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
            }
        }

        /// <summary>
        /// Private helper method. Adds Roles to the database.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an AsyncInnDbContext object
        /// </param>
        private static void AddRoles(AsyncInnDbContext context)
        {
            if (context.Roles.Any()) return;
            foreach (IdentityRole role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
