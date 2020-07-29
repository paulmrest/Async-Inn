using Async_Inn.Data;
using Async_Inn.Models.DTOs;
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
        /// <param name="amenity">
        /// IAmenity: an object that implements IAmenity
        /// </param>
        /// <returns>
        /// Task<List<RoomDTO>>: a List of RoomDTOs embedded in a Task object
        /// </returns>
        public async Task<List<RoomDTO>> GetRooms(IAmenity amenity)
        {
            List<Room> rooms = await _context.Rooms
                .Include(x => x.RoomAmenities)
                .ToListAsync();
            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (Room oneRoom in rooms)
            {
                RoomDTO newRoomDTO = new RoomDTO() { Id = oneRoom.Id, Name = oneRoom.Name, Layout = oneRoom.Layout };
                List<AmenityDTO> amenityDTOs = new List<AmenityDTO>();
                foreach (RoomAmenities oneAmenity in oneRoom.RoomAmenities)
                {
                    amenityDTOs.Add(await amenity.GetAmenity(oneAmenity.AmenityId));
                }
                newRoomDTO.Amenities = amenityDTOs;
                roomDTOs.Add(newRoomDTO);
            }
            return roomDTOs;
        }

        /// <summary>
        /// Gets a specific room by id.
        /// </summary>
        /// <param name="id">
        /// int: a Room id
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements IAmenity
        /// </param>
        /// <returns>
        /// Task<RoomDTO>: a RoomDTO object embedded in a Task object
        /// </returns>
        public async Task<RoomDTO> GetRoom(int id, IAmenity amenity)
        {

            Room room = await _context.Rooms
                .Where(x => x.Id == id)                           
                .Include(x => x.RoomAmenities)     
                .FirstOrDefaultAsync();

            RoomDTO roomDTO = await _context.Rooms
                .Where(x => x.Id == id)
                .Select(x => new RoomDTO() { Id = x.Id, Name = x.Name, Layout = x.Layout })                                  
                .FirstOrDefaultAsync();
            List<AmenityDTO> amenityDTOs = new List<AmenityDTO>();
            foreach (RoomAmenities oneAmenity in room.RoomAmenities)
            {
                amenityDTOs.Add(await amenity.GetAmenity(oneAmenity.AmenityId));
            }
            roomDTO.Amenities = amenityDTOs;
            return roomDTO;
        }

        /// <summary>
        /// Adds a Room object to the database.
        /// </summary>
        /// <param name="room">
        /// RoomDTO: a RoomDTO object
        /// </param>
        /// <returns>
        /// Task<RoomDTO>: the parameter RoomDTO object after being added to the database, embedded in a Task object
        /// </returns>
        public async Task<RoomDTO> Create(RoomDTO roomDTO)
        {
            Room roomEntity = new Room()
            {
                Name = roomDTO.Name,
                Layout = roomDTO.Layout
            };
            _context.Entry(roomEntity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            roomDTO.Id = roomEntity.Id;
            return roomDTO;
        }

        /// <summary>
        /// Updates a room in the database.
        /// </summary>
        /// <param name="room">
        /// RoomDTO: the RoomDTO object with updated information
        /// </param>
        /// <returns>
        /// Task<RoomDTO>: the updated RoomDTO object embedded in a Task object
        /// </returns>
        public async Task<RoomDTO> Update(RoomDTO roomDTO)
        {
            Room roomEntity = new Room()
            {
                Id = roomDTO.Id,
                Name = roomDTO.Name,
                Layout = roomDTO.Layout
            };
            _context.Entry(roomEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomDTO;
        }

        /// <summary>
        /// Deletes a room from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Room to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            Room roomEntity = await _context.Rooms
                .FindAsync(id);
            _context.Entry(roomEntity).State = EntityState.Deleted;
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
            var result = await _context.RoomAmenities
                .FirstOrDefaultAsync(x => x.AmenityId == amenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
