using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel Corona",
                    StreetAddress = "123 PPE Lane",
                    City = "Nada",
                    State = "UB",
                    Phone = "(555) 555-5555"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel Corona 2",
                    StreetAddress = "321 PPE Lane",
                    City = "Nada",
                    State = "UB",
                    Phone = "(555) 555-5555"
                }
            );
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
