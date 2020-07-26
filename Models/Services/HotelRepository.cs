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

        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<List<Hotel>> GetHotels()
        {
            var students = await _context.Hotels.ToListAsync();
            return students;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels.Where(x => x.Id == id)
                                               .FirstAsync();
            var hotelRoomsForHotel = await _context.HotelRooms.Where(x => x.HotelId == id)
                                                              .Include(x => x.Room)
                                                              .ToListAsync();
            hotel.HotelRooms = hotelRoomsForHotel;
            return hotel;
        }

        public async Task<Hotel> GetHotelByName(string hotelName)
        {
            int hotelId = await _context.Hotels.Where(x => x.Name == hotelName)
                                               .Select(x => x.Id)
                                               .FirstAsync();
            return await GetHotel(hotelId);
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
