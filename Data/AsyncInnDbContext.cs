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
                    Name = "Launch Window Inn",
                    StreetAddress = "1234 Main St",
                    City = "San Diego",
                    State = "CA",
                    Phone = "(555) 555-5555"
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel Rona",
                    StreetAddress = "4321 Walnut Ave",
                    City = "Kansas City",
                    State = "MO",
                    Phone = "(555) 555-5555"
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Middlewear Motel",
                    StreetAddress = "9876 Messy Ln",
                    City = "Tampa",
                    State = "FL",
                    Phone = "(555) 555-5555"
                }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "The Janitor's Closet",
                    Layout = 10
                },
                new Room
                {
                    Id = 2,
                    Name = "A Windy Tower",
                    Layout = 6
                },
                new Room
                {
                    Id = 3,
                    Name = "Upside Down Tree",
                    Layout = 10
                }
            );

            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom
                {
                    HotelId = 1,
                    RoomNumber = 56,
                    RoomId = 1,
                    Rate = 86.66m,
                    PetFriendly = false
                },
                new HotelRoom
                {
                    HotelId = 2,
                    RoomNumber = 12,
                    RoomId = 2,
                    Rate = 132.19m,
                    PetFriendly = true
                },
                new HotelRoom
                {
                    HotelId = 3,
                    RoomNumber = 245,
                    RoomId = 3,
                    Rate = 50.78m,
                    PetFriendly = false
                }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "Batcave"
                },
                new Amenity
                {
                    Id = 2,
                    Name = "Ceiling Pillows"
                },
                new Amenity
                {
                    Id = 3,
                    Name = "Inside-out-microwave"
                }
            );

            modelBuilder.Entity<RoomAmenities>().HasData(
                new RoomAmenities
                {
                    AmenityId = 1,
                    RoomId = 1
                },
               new RoomAmenities
               {
                   AmenityId = 2,
                   RoomId = 2
               },
               new RoomAmenities
               {
                   AmenityId = 3,
                   RoomId = 3
               }
            );

            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });

            modelBuilder.Entity<RoomAmenities>().HasKey(x => new { x.AmenityId, x.RoomId });
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<RoomAmenities> RoomAmenities { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }
    }
}