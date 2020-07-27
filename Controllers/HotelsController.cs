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
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        /// <summary>
        /// Instantiates a new HotelsController object.
        /// </summary>
        /// <param name="hotel">
        /// IHotel: a repository object that implements the IHotel interface
        /// </param>
        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: /api/Hotels
        [HttpGet("/api/Hotels")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels(int id)
        {
            return await _hotel.GetHotels();
        }

        // GET: /api/Hotels/{id}
        [HttpGet("/api/Hotels/{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            Hotel hotel = await _hotel.GetHotel(id);
            return hotel;
        }

        // GET: /api/Hotels/HotelByName/{hotelName}
        [HttpGet("/api/Hotels/HotelByName/{hotelName}")]
        public async Task<ActionResult<Hotel>> GetHotelByName(string hotelName)
        {
            Hotel hotel = await _hotel.GetHotelByName(hotelName);
            return hotel;
        }

        // PUT: /api/Hotels/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            var updatedhotel = await _hotel.Update(hotel);
            return Ok(updatedhotel);
        }

        // POST: /api/Hotels/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Hotels/{id}")]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _hotel.Create(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: /api/Hotels/{id}
        [HttpDelete("/api/Hotels/{id}")]
        public async Task<ActionResult<Hotel>> DeleteHotel(int id)
        {
            await _hotel.Delete(id);
            return NoContent();
        }
    }
}
