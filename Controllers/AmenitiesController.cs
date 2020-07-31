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
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = ApplicationRoles.DistrictManager)]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenity _amenity;
        private readonly UserManager<ApplicationUser> _userManager;

        public AmenitiesController(UserManager<ApplicationUser> userManager, IAmenity amenity)
        {
            _amenity = amenity;
            _userManager = userManager;
        }

        // GET: api/Amenities
        [AllowAnonymous]
        [HttpGet("/api/Amenities")]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            return await _amenity.GetAmenities();
        }

        // GET: /api/Amenities/{id}
        [AllowAnonymous]
        [HttpGet("/api/Amenities/{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
            return await _amenity.GetAmenity(id);
        }

        // POST: /api/Amenities/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = ApplicationRoles.PropertyManager)]
        [HttpPost("/api/Amenities")]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenityDTO)
        {
            await _amenity.Create(amenityDTO);
            return CreatedAtAction("GetAmenity", new { id = amenityDTO.Id }, amenityDTO);
        }

        // PUT: /api/Amenities/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = ApplicationRoles.PropertyManager)]
        [HttpPut("/api/Amenities/{id}")]
        public async Task<IActionResult> PutAmenity(int id, AmenityDTO amenityDTO)
        {
            if (id != amenityDTO.Id)
            {
                return BadRequest();
            }
            AmenityDTO updatedAmenity = await _amenity.Update(amenityDTO);
            return Ok(updatedAmenity);
        }

        // DELETE: /api/Amenities/{id}
        [Authorize(Roles = ApplicationRoles.PropertyManager)]
        [HttpDelete("/api/Amenities/{id}")]
        public async Task<ActionResult<AmenityDTO>> DeleteAmenity(int id)
        {
            await _amenity.Delete(id);
            return NoContent();
        }
    }
}
