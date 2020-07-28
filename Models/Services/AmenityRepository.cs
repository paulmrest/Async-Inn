using Async_Inn.Data;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class AmenityRepository : IAmenity
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Instantiates a new AmenityRepository object.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an object that inherits from DbContext
        /// </param>
        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all amenities.
        /// </summary>
        /// <returns>
        /// Task<List<AmenityDTO>>: a List of AmenityDTO objects embedded in a Task object
        /// </returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            List<AmenityDTO> amenities = await _context.Amenities.Select(x => new AmenityDTO { Id = x.Id, Name = x.Name })
                                                                 .ToListAsync();
            return amenities;
        }

        /// <summary>
        /// Gets a specific amenity by its id.
        /// </summary>
        /// <param name="id">
        /// int: an AmenityDTO id
        /// </param>
        /// <returns>
        /// Task<Amenity>: an AmenityDTO object embedded in a Task object
        /// </returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            AmenityDTO amenity = await _context.Amenities.Where(x => x.Id == id)
                                                         .Select(x => new AmenityDTO { Id = x.Id, Name = x.Name })
                                                         .FirstOrDefaultAsync();
            return amenity;
        }

        /// <summary>
        /// Saves an amenity to the database.
        /// </summary>
        /// <param name="amenity">
        /// AmenityDTO: the AmenityDTO object to be saved to the database
        /// </param>
        /// <returns>
        /// Task<AmenityDTO>: the parameter AmenityDTO object updated after saving to the database, embedded in a Task object
        /// </returns>
        public async Task<AmenityDTO> Create(AmenityDTO amenityDTO)
        {
            Amenity amenityEntity = new Amenity()
            {
                Name = amenityDTO.Name
            };
            _context.Entry(amenityEntity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            amenityDTO.Id = amenityEntity.Id;
            return amenityDTO;
        }

        /// <summary>
        /// Updates an amenity in the database.
        /// </summary>
        /// <param name="amenity">
        /// AmenityDTO: an AmenityDTO object with the updated information
        /// </param>
        /// <returns>
        /// Task<AmenityDTO>: the updated AmenityDTO object embedded in a Task object
        /// </returns>
        public async Task<AmenityDTO> Update(AmenityDTO amenityDTO)
        {
            Amenity amenityEntity = new Amenity()
            {
                Id = amenityDTO.Id,
                Name = amenityDTO.Name
            };
            _context.Entry(amenityEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenityDTO;
        }

        /// <summary>
        /// Deletes an amenity from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the AmenityDTO object to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            AmenityDTO amenityDTO = await GetAmenity(id);
            Amenity amenityEntity = new Amenity()
            {
                Id = amenityDTO.Id,
                Name = amenityDTO.Name
            };
            _context.Entry(amenityEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
