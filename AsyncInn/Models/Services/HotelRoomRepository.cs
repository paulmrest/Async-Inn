using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<List<HotelRoomDTO>> GetHotelRooms(IRoom room, IAmenity amenity)
        {
            List<HotelRoomDTO> hotelRoomDTOs = await _context.HotelRooms
                .Select(x => new HotelRoomDTO() 
                { 
                    HotelId = x.HotelId, 
                    RoomNumber = x.RoomNumber, 
                    RoomId = x.RoomId, 
                    Rate = x.Rate, 
                    PetFriendly = x.PetFriendly 
                })
                .ToListAsync();
            if (hotelRoomDTOs.Count > 1)
            {
                foreach (HotelRoomDTO oneHotelRoomDTO in hotelRoomDTOs)
                {
                    oneHotelRoomDTO.Room = await room.GetRoom(oneHotelRoomDTO.RoomId, amenity);
                }
            }
            return hotelRoomDTOs;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRoomsForHotel(int hotelId, IRoom room, IAmenity amenity)
        {
            List<HotelRoomDTO> hotelRoomDTOsForHotel = await _context.HotelRooms
                .Where(x => x.HotelId == hotelId)
                .Select(x => new HotelRoomDTO()
                {
                    HotelId = x.HotelId,
                    RoomNumber = x.RoomNumber,
                    RoomId = x.RoomId,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly
                })
                .ToListAsync();
            if (hotelRoomDTOsForHotel.Count < 1)
            {
                return null;
            }
            foreach (HotelRoomDTO oneHotelRoomDTO in hotelRoomDTOsForHotel)
            {
                oneHotelRoomDTO.Room = await room.GetRoom(oneHotelRoomDTO.RoomId, amenity);
            }
            return hotelRoomDTOsForHotel;
        }

        public async Task<HotelRoomDTO> GetRoomDetails(int hotelId, int roomNumber, IRoom room, IAmenity amenity)
        {
            HotelRoomDTO hotelRoomDTO = await _context.HotelRooms
                .Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                .Select(x => new HotelRoomDTO()
                {
                    HotelId = x.HotelId,
                    RoomNumber = x.RoomNumber,
                    RoomId = x.RoomId,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly
                })
                .FirstOrDefaultAsync();
            if (hotelRoomDTO == null)
            {
                return null;
            }
            hotelRoomDTO.Room = await room.GetRoom(hotelId, amenity);
            return hotelRoomDTO;
        }

        public async Task<HotelRoomDTO> GetHotelRoomWithoutDetails(int hotelId, int roomNumber)
        {
            HotelRoomDTO hotelRoomDTO = await _context.HotelRooms
                .Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                .Select(x => new HotelRoomDTO()
                {
                    HotelId = x.HotelId,
                    RoomNumber = x.RoomNumber,
                    RoomId = x.RoomId,
                    Rate = x.Rate,
                    PetFriendly = x.PetFriendly
                })
                .FirstOrDefaultAsync();
            if (hotelRoomDTO == null)
            {
                return null;
            }
            return hotelRoomDTO;
        }

        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO, int hotelId)
        {
            HotelRoom hotelRoomEntity = new HotelRoom()
            {
                HotelId = hotelRoomDTO.HotelId,
                RoomNumber = hotelRoomDTO.RoomNumber,
                RoomId = hotelRoomDTO.RoomId,
                Rate = hotelRoomDTO.Rate,
                PetFriendly = hotelRoomDTO.PetFriendly
            };
            _context.Entry(hotelRoomEntity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hotelRoomDTO;
        }

        public async Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoomEntity = new HotelRoom()
            {
                HotelId = hotelRoomDTO.HotelId,
                RoomNumber = hotelRoomDTO.RoomNumber,
                RoomId = hotelRoomDTO.RoomId,
                Rate = hotelRoomDTO.Rate,
                PetFriendly = hotelRoomDTO.PetFriendly
            };
            _context.Entry(hotelRoomEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoomDTO;
        }

        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
