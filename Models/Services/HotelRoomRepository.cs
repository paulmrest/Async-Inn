using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Instantiates a new HotelRoomRepository object.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an object that inherits from DbContext
        /// </param>
        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all hotel rooms.
        /// </summary>
        /// <returns>
        /// Task<List<HotelRoom>>: a List of HotelRooms embedded in a Task object
        /// </returns>
        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var hotelRooms = await _context.HotelRooms.ToListAsync();
            return hotelRooms;
        }

        /// <summary>
        /// Gets a specific hotel room by hotelId and roomNumber WITHOUT any connected objects.
        /// </summary>
        /// <param name="hotelId">
        /// int: a hotelId
        /// </param>
        /// <param name="roomNumber">
        /// int: a roomNumber
        /// </param>
        /// <returns>
        /// Task<HotelRoom>: a single HotelRoom object embedded in a Task object
        /// </returns>
        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            return hotelRoom;
        }

        /// <summary>
        /// Gets all hotel rooms for a given hotel.
        /// </summary>
        /// <param name="hotelId">
        /// int: a hotelId
        /// </param>
        /// <returns>
        /// Task<List<HotelRoom>>: a List of HotelRooms embedded in a Task object
        /// </returns>
        public async Task<List<HotelRoom>> GetHotelRoomsForHotel(int hotelId)
        {
            var hotelRoomsForHotel = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                              .Include(x => x.Hotel)
                                                              .Include(x => x.Room)
                                                              .ThenInclude(x => x.RoomAmenities)
                                                              .ThenInclude(x => x.Amenity)
                                                              .ToListAsync();
            return hotelRoomsForHotel;
        }

        /// <summary>
        /// Gets a specific HotelRoom by hotelId and roomNumber WITH all connected objects.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns></returns>
        public async Task<HotelRoom> GetRoomDetails(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                     .Include(x => x.Hotel)
                                                     .Include(x => x.Room)
                                                     .ThenInclude(x => x.RoomAmenities)
                                                     .ThenInclude(x => x.Amenity)
                                                     .FirstAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Adds a HotelRoom to the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoom: a HotelRoom object
        /// </param>
        /// <returns>
        /// Task<HotelRoom>: the parameter HotelRoom object after saving it to the database
        /// </returns>
        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Updates a HotelRoom in the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoom: the HotelRoom to be updated
        /// </param>
        /// <returns>
        /// Task<HotelRoom>: the parameter HotelRoom object after updating it in the database
        /// </returns>
        public async Task<HotelRoom> Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        /// <summary>
        /// Deletes a HotelRoom from the database by its hotelId and roomNumber.
        /// </summary>
        /// <param name="hotelId">
        /// int: the hotelId of HotelRoom to be deleted
        /// </param>
        /// <param name="roomNumber">
        /// int: the roomNumber of HotelRoom to be deleted
        /// </param>
        /// <returns></returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
