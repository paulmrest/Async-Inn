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

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            List<AmenityDTO> amenities = await _context.Amenities
                .Select(x => new AmenityDTO { Id = x.Id, Name = x.Name })
                .ToListAsync();
            return amenities;
        }

        public async Task<AmenityDTO> GetAmenity(int id)
        {
            AmenityDTO amenity = await _context.Amenities
                .Where(x => x.Id == id)              
                .Select(x => new AmenityDTO { Id = x.Id, Name = x.Name })                                         
                .FirstOrDefaultAsync();
            return amenity;
        }

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

        public async Task Delete(int id)
        {
            Amenity amenityEntity = await _context.Amenities
                .FindAsync(id);  
            _context.Entry(amenityEntity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
