using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<List<RoomDTO>> GetRooms(IAmenity amenity);

        Task<RoomDTO> GetRoom(int id, IAmenity amenity);

        Task<RoomDTO> Create(RoomDTO roomDTO);

        Task<RoomDTO> Update(RoomDTO roomDTO);

        Task Delete(int id);

        Task AddAmenityToRoom(int amenityId, int roomId);

        Task RemoveAmenityFromRoom(int amenityId, int roomId);
    }
}
