﻿// <auto-generated />
using Async_Inn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Async_Inn.Migrations
{
    [DbContext(typeof(AsyncInnDbContext))]
    [Migration("20200723225049_ChangedAmenities")]
    partial class ChangedAmenities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Async_Inn.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Batcave"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ceiling Pillows"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Inside-out-microwave"
                        });
                });

            modelBuilder.Entity("Async_Inn.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "San Diego",
                            Name = "Launch Window Inn",
                            Phone = "(555) 555-5555",
                            State = "CA",
                            StreetAddress = "1234 Main St"
                        },
                        new
                        {
                            Id = 2,
                            City = "Kansas City",
                            Name = "Hotel Rona",
                            Phone = "(555) 555-5555",
                            State = "MO",
                            StreetAddress = "4321 Walnut Ave"
                        },
                        new
                        {
                            Id = 3,
                            City = "Tampa",
                            Name = "Middlewear Motel",
                            Phone = "(555) 555-5555",
                            State = "FL",
                            StreetAddress = "9876 Messy Ln"
                        });
                });

            modelBuilder.Entity("Async_Inn.Models.HotelRoom", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<bool>("PetFriendly")
                        .HasColumnType("bit");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("HotelId", "RoomNumber");

                    b.HasIndex("RoomId");

                    b.ToTable("HotelRoom");
                });

            modelBuilder.Entity("Async_Inn.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 10,
                            Name = "The Janitor's Closet"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 6,
                            Name = "A Windy Tower"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 10,
                            Name = "Upside Down Tree"
                        });
                });

            modelBuilder.Entity("Async_Inn.Models.RoomAmenities", b =>
                {
                    b.Property<int>("AmenityId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("AmenityId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomAmenities");
                });

            modelBuilder.Entity("Async_Inn.Models.HotelRoom", b =>
                {
                    b.HasOne("Async_Inn.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Async_Inn.Models.Room", "Room")
                        .WithMany("HotelRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Async_Inn.Models.RoomAmenities", b =>
                {
                    b.HasOne("Async_Inn.Models.Amenity", "Amenity")
                        .WithMany("RoomAmenities")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Async_Inn.Models.Room", "Room")
                        .WithMany("Amenities")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
