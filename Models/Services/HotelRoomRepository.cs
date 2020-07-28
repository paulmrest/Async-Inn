using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

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
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<List<HotelRoomDTO>>: a List of HotelRoomDTOs embedded in a Task object
        /// </returns>
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

            foreach (HotelRoomDTO oneHotelRoomDTO in hotelRoomDTOs)
            {
                oneHotelRoomDTO.Room = await room.GetRoom(oneHotelRoomDTO.RoomId, amenity);
            }
            return hotelRoomDTOs;
        }

        /// <summary>
        /// Gets all hotel rooms for a given hotel.
        /// </summary>
        /// <param name="hotelId">
        /// int: a hotelId
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<List<HotelRoomDTO>>: a List of HotelRoomDTOs embedded in a Task object
        /// </returns>
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
            foreach (HotelRoomDTO oneHotelRoomDTO in hotelRoomDTOsForHotel)
            {
                oneHotelRoomDTO.Room = await room.GetRoom(oneHotelRoomDTO.RoomId, amenity);
            }
            return hotelRoomDTOsForHotel;
        }

        /// <summary>
        /// Gets a specific hotel room by hotelId and roomNumber with connected rooms and amenities.
        /// </summary>
        /// <param name="hotelId">
        /// int: a hotelId
        /// </param>
        /// <param name="roomNumber">
        /// int: a roomNumber
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<HotelRoom>: a single HotelRoomDTO embedded in a Task object
        /// </returns>
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
            hotelRoomDTO.Room = await room.GetRoom(hotelId, amenity);
            return hotelRoomDTO;
        }


        /// <summary>
        /// Gets a specific HotelRoom by hotelId and roomNumber with no details.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>
        /// Task<HotelRoom>: a HotelRoom object embedded in a Task object
        /// </returns>
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
            return hotelRoomDTO;
        }

        /// <summary>
        /// Adds a HotelRoom to the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoomDTO: a HotelRoomDTO object
        /// </param>
        /// <returns>
        /// Task<HotelRoomDTO>: the parameter HotelRoomDTO object after saving it to the database
        /// </returns>
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

        /// <summary>
        /// Updates a HotelRoom in the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoomDTO: the HotelRoomDTO to be updated
        /// </param>
        /// <returns>
        /// Task<HotelRoomDTO>: the parameter HotelRoomDTO object after updating it in the database
        /// </returns>
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

        /// <summary>
        /// Deletes a HotelRoom from the database by its hotelId and roomNumber.
        /// </summary>
        /// <param name="hotelId">
        /// int: the hotelId of HotelRoom to be deleted
        /// </param>
        /// <param name="roomNumber">
        /// int: the roomNumber of HotelRoom to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
