using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <param name="amenity">
        /// IAmenity: an object that implements IAmenity
        /// </param>
        /// <returns>
        /// Task<List<RoomDTO>>: a List of RoomDTOs embedded in a Task object
        /// </returns>
        Task<List<RoomDTO>> GetRooms(IAmenity amenity);

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
        Task<RoomDTO> GetRoom(int id, IAmenity amenity);

        /// <summary>
        /// Adds a Room object to the database.
        /// </summary>
        /// <param name="room">
        /// RoomDTO: a RoomDTO object
        /// </param>
        /// <returns>
        /// Task<RoomDTO>: the parameter RoomDTO object after being added to the database, embedded in a Task object
        /// </returns>
        Task<RoomDTO> Create(RoomDTO roomDTO);

        /// <summary>
        /// Updates a room in the database.
        /// </summary>
        /// <param name="room">
        /// RoomDTO: the RoomDTO object with updated information
        /// </param>
        /// <returns>
        /// Task<RoomDTO>: the updated RoomDTO object embedded in a Task object
        /// </returns>
        Task<RoomDTO> Update(RoomDTO roomDTO);

        /// <summary>
        /// Deletes a room from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Room to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        Task Delete(int id);

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
        Task AddAmenityToRoom(int amenityId, int roomId);

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
        Task RemoveAmenityFromRoom(int amenityId, int roomId);
    }
}
