using Async_Inn.Data;
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
        /// Task<List<Amenity>>: a List of Amenity objects embedded in a Task object
        /// </returns>
        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        /// <summary>
        /// Gets a specific Amenity by its id.
        /// </summary>
        /// <param name="id">
        /// int: an Amenity id
        /// </param>
        /// <returns>
        /// Task<Amenity>: an Amenity object embedded in a Task object
        /// </returns>
        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            var amenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                        .Include(x => x.Room)
                                                        .ToListAsync();
            amenity.RoomAmenities = amenities;
            return amenity;
        }

        /// <summary>
        /// Saves an Amenity object to the database.
        /// </summary>
        /// <param name="amenity">
        /// Amenity: the Amenity object to be saved to the database
        /// </param>
        /// <returns>
        /// Task<Amenity>: the parameter Amenity object updated after saving to the database, embedded in a Task object
        /// </returns>
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// Updates an Amenity object in the database.
        /// </summary>
        /// <param name="amenity">
        /// Amenity: an Amenity object with the updated information
        /// </param>
        /// <returns>
        /// Task<Amenity>: the updated Amenity object embedded in a Task object
        /// </returns>
        public async Task<Amenity> Update(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// Deletes an Amenity object from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Amenity object to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
