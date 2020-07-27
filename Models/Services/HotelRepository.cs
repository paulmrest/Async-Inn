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
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Instantiates a new HotelRepository object.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an object that inherits from DbContext
        /// </param>
        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Hotels.
        /// </summary>
        /// <returns>
        /// Task<List<Hotel>>: a list of Hotel objects embedded in a Task object
        /// </returns>
        public async Task<List<Hotel>> GetHotels()
        {
            var students = await _context.Hotels.ToListAsync();
            return students;
        }

        /// <summary>
        /// Gets a Hotel by id.
        /// </summary>
        /// <param name="id">
        /// int: a Hotel id
        /// </param>
        /// <returns>
        /// Task<Hotel>: a Hotel object embedded in a Task object
        /// </returns>
        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels.Where(x => x.Id == id)
                                               .FirstAsync();
            var hotelRoomsForHotel = await _context.HotelRooms.Where(x => x.HotelId == id)
                                                              .Include(x => x.Room)
                                                              .ThenInclude(x => x.RoomAmenities)
                                                              .Include(x => x.Room)
                                                              .ThenInclude(x => x.HotelRooms)
                                                              .ToListAsync();
            hotel.HotelRooms = hotelRoomsForHotel;
            return hotel;
        }

        /// <summary>
        /// Gets a Hotel by name.
        /// </summary>
        /// <param name="hotelName">
        /// string: a hotel name
        /// </param>
        /// <returns>
        /// Task<Hotel>: a Hotel object embedded in a Task object
        /// </returns>
        public async Task<Hotel> GetHotelByName(string hotelName)
        {
            int hotelId = await _context.Hotels.Where(x => x.Name == hotelName)
                                               .Select(x => x.Id)
                                               .FirstAsync();
            return await GetHotel(hotelId);
        }

        /// <summary>
        /// Saves a Hotel object to the database.
        /// </summary>
        /// <param name="hotel">
        /// Hotel: a Hotel object
        /// </param>
        /// <returns>
        /// Task<Hotel>: the parameter Hotel object after being saved to the database
        /// </returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Updates a Hotel object in the database.
        /// </summary>
        /// <param name="hotel">
        /// Hotel: a Hotel object with updated information
        /// </param>
        /// <returns>
        /// Task<Hotel>: the parameter Hotel object after being updated
        /// </returns>
        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Deletes a Hotel object from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Hotel to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
