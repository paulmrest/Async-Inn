using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _amenity;

        public AmenitiesController(IAmenity amenity)
        {
            _amenity = amenity;
        }

        // GET: api/Amenities
        [HttpGet("/api/Amenities")]
        public async Task<ActionResult<IEnumerable<Amenity>>> GetAmenities()
        {
            return await _amenity.GetAmenities();
        }

        // GET: /api/Amenities/{id}
        [HttpGet("/api/Amenities/{id}")]
        public async Task<ActionResult<Amenity>> GetAmenity(int id)
        {
            Amenity amenity = await _amenity.GetAmenity(id);
            return amenity;
        }

        // PUT: /api/Amenities/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Amenities/{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }
            var updatedAmenity = await _amenity.Update(amenity);
            return Ok(updatedAmenity);
        }

        // POST: /api/Amenities/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Amenities/{id}")]
        public async Task<ActionResult<Amenity>> PostAmenity(Amenity amenity)
        {
            await _amenity.Create(amenity);
            return CreatedAtAction("GetAmenity", new { id = amenity.Id }, amenity);
        }

        // DELETE: /api/Amenities/{id}
        [HttpDelete("/api/Amenities/{id}")]
        public async Task<ActionResult<Amenity>> DeleteAmenity(int id)
        {
            await _amenity.Delete(id);
            return NoContent();
        }
    }
}
