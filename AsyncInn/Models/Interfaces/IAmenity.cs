using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// Gets all amenities.
        /// </summary>
        /// <returns>
        /// Task<List<AmenityDTO>>: a List of AmenityDTO objects embedded in a Task object
        /// </returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// Gets a specific amenity by its id.
        /// </summary>
        /// <param name="id">
        /// int: an AmenityDTO id
        /// </param>
        /// <returns>
        /// Task<Amenity>: an AmenityDTO object embedded in a Task object
        /// </returns>
        Task<AmenityDTO> GetAmenity(int id);

        /// <summary>
        /// Saves an amenity to the database.
        /// </summary>
        /// <param name="amenity">
        /// AmenityDTO: the AmenityDTO object to be saved to the database
        /// </param>
        /// <returns>
        /// Task<AmenityDTO>: the parameter AmenityDTO object updated after saving to the database, embedded in a Task object
        /// </returns>
        Task<AmenityDTO> Create(AmenityDTO amenity);

        /// <summary>
        /// Updates an amenity in the database.
        /// </summary>
        /// <param name="amenity">
        /// AmenityDTO: an AmenityDTO object with the updated information
        /// </param>
        /// <returns>
        /// Task<AmenityDTO>: the updated AmenityDTO object embedded in a Task object
        /// </returns>
        Task<AmenityDTO> Update(AmenityDTO amenityDTO);

        /// <summary>
        /// Deletes an amenity from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the AmenityDTO object to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        Task Delete(int id);
    }
}
