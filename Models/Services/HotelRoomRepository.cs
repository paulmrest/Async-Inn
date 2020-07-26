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

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<List<HotelRoom>> GetHotelRooms()
        {
            var hotelRooms = await _context.HotelRooms.ToListAsync();
            return hotelRooms;
        }

        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            return hotelRoom;
        }

        public async Task<List<HotelRoom>> GetHotelRoomsForHotel(int hotelId)
        {
            var hotelRoomsForHotel = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                              .Include(x => x.Room)
                                                              .ThenInclude(x => x.RoomAmenities)
                                                              .ThenInclude(x => x.Amenity).ToListAsync();
            return hotelRoomsForHotel;
        }

        public async Task<HotelRoom> GetRoomDetails(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                     .Include(x => x.Room)
                                                     .ThenInclude(x => x.RoomAmenities)
                                                     .ThenInclude(x => x.Amenity).FirstAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
