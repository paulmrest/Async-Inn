using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
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
        Task<List<HotelRoomDTO>> GetHotelRooms(IRoom room, IAmenity amenity);

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
        Task<List<HotelRoomDTO>> GetHotelRoomsForHotel(int hotelId, IRoom room, IAmenity amenity);

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
        Task<HotelRoomDTO> GetRoomDetails(int hotelId, int roomNumber, IRoom room, IAmenity amenity);

        /// <summary>
        /// Gets a specific HotelRoom by hotelId and roomNumber with no details.
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomNumber"></param>
        /// <returns>
        /// Task<HotelRoom>: a HotelRoom object embedded in a Task object
        /// </returns>
        Task<HotelRoomDTO> GetHotelRoomWithoutDetails(int hotelId, int roomNumber);

        /// <summary>
        /// Adds a HotelRoom to the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoomDTO: a HotelRoomDTO object
        /// </param>
        /// <returns>
        /// Task<HotelRoomDTO>: the parameter HotelRoomDTO object after saving it to the database
        /// </returns>
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO, int hotelId);

        /// <summary>
        /// Updates a HotelRoom in the database.
        /// </summary>
        /// <param name="hotelRoom">
        /// HotelRoomDTO: the HotelRoomDTO to be updated
        /// </param>
        /// <returns>
        /// Task<HotelRoomDTO>: the parameter HotelRoomDTO object after updating it in the database
        /// </returns>
        Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoomDTO);

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
        Task Delete(int hotelId, int roomNumber);
    }
}
