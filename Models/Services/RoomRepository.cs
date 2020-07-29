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

        public async Task Delete(int id)
        {
            Room roomEntity = await _context.Rooms
                .FindAsync(id);
            _context.Entry(roomEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

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

        public async Task RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            var result = await _context.RoomAmenities
                .FirstOrDefaultAsync(x => x.AmenityId == amenityId && x.RoomId == roomId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
