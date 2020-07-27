using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class RoomRepository : IRoom
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Instantiates a new RoomRepository object.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an object that inherits from DbContext
        /// </param>
        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns>
        /// Task<List<Room>>: a List of Rooms embedded in a Task object
        /// </returns>
        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return rooms;
        }

        /// <summary>
        /// Gets a specific room by id.
        /// </summary>
        /// <param name="id">
        /// int: a Room id
        /// </param>
        /// <returns>
        /// Task<Room>: a Room object embedded in a Task object
        /// </returns>
        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            var amenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                        .Include(x => x.Amenity)
                                                        .ToListAsync();
            room.RoomAmenities = amenities;
            return room;
        }

        /// <summary>
        /// Adds a Room object to the database.
        /// </summary>
        /// <param name="room">
        /// Room: a Room object
        /// </param>
        /// <returns>
        /// Task<Room>: the parameter Room object after being added to the database, embedded in a Task object
        /// </returns>
        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Updates a Room object in the database.
        /// </summary>
        /// <param name="room">
        /// Room: the Room object with updated information
        /// </param>
        /// <returns>
        /// Task<Room>: the updated Room object embedded in a Task object
        /// </returns>
        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Deletes a Room object from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Room to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an Amenity to a Room
        /// </summary>
        /// <param name="amenityId">
        /// int: an Amenity id
        /// </param>
        /// <param name="roomId">
        /// int: a Room id
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task AddAmenityToRoom(int amenityId, int roomId)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };
            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an Amenity from a Room.
        /// </summary>
        /// <param name="amenityId">
        /// int: an Amenity id
        /// </param>
        /// <param name="roomId">
        /// int: a Room id
        /// </param>
        /// <returns>
        /// Task: an Empty Task object
        /// </returns>
        public async Task RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(x => x.AmenityId == amenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
