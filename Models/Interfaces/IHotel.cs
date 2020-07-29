using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// Gets all Hotels.
        /// </summary>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<List<HotelDTO>>: a list of HotelDTO objects embedded in a Task object
        /// </returns>
        Task<List<HotelDTO>> GetHotels(IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        /// <summary>
        /// Gets a Hotel by id.
        /// </summary>
        /// <param name="id">
        /// int: a Hotel id
        /// </param>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: a HotelDTO object embedded in a Task object
        /// </returns>
        Task<HotelDTO> GetHotel(int id, IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        /// <summary>
        /// Gets a Hotel by name.
        /// </summary>
        /// <param name="hotelName">
        /// string: a hotel name
        /// </param>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: a HotelDTO object embedded in a Task object
        /// </returns>
        Task<HotelDTO> GetHotelByName(string hotelName, IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        /// <summary>
        /// Saves a Hotel object to the database.
        /// </summary>
        /// <param name="hotel">
        /// HotelDTO: a HotelDTO object
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: the parameter HotelDTO object after being saved to the database
        /// </returns>
        Task<HotelDTO> Create(HotelDTO hotelDTO);

        /// <summary>
        /// Updates a Hotel object in the database.
        /// </summary>
        /// <param name="hotel">
        /// HotelDTO: a HotelDTO object with updated information
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: the parameter HotelDTO object after being updated
        /// </returns>
        Task<HotelDTO> Update(HotelDTO hotelDTO);

        /// <summary>
        /// Deletes a Hotel object from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Hotel to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        Task Delete(int id);
    }
}
