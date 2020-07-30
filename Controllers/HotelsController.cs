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

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;
        private readonly IHotelRoom _hotelRoom;
        private readonly IRoom _room;
        private readonly IAmenity _amenity;

        /// <summary>
        /// Instantiates a new HotelsController object.
        /// </summary>
        /// <param name="hotel">
        /// IHotel: a repository object that implements the IHotel interface
        /// </param>
        public HotelsController(IHotel hotel, IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            _hotel = hotel;
            _hotelRoom = hotelRoom;
            _room = room;
            _amenity = amenity;
        }

        // GET: /api/Hotels
        [HttpGet("/api/Hotels")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels(int id)
        {
            return await _hotel.GetHotels(_hotelRoom, _room, _amenity);
        }

        // GET: /api/Hotels/{id}
        [HttpGet("/api/Hotels/{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            HotelDTO hotelDTO = await _hotel.GetHotel(id, _hotelRoom, _room, _amenity);
            return hotelDTO;
        }

        // GET: /api/Hotels/HotelByName/{hotelName}
        [HttpGet("/api/Hotels/HotelByName/{hotelName}")]
        public async Task<ActionResult<HotelDTO>> GetHotelByName(string hotelName)
        {
            HotelDTO hotelDTO = await _hotel.GetHotelByName(hotelName, _hotelRoom, _room, _amenity);
            return hotelDTO;
        }

        // POST: /api/Hotels/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Hotels")]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotelDTO)
        {
            await _hotel.Create(hotelDTO);
            return CreatedAtAction("GetHotel", new { id = hotelDTO.Id }, hotelDTO);
        }

        // PUT: /api/Hotels/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.Id)
            {
                return BadRequest();
            }
            HotelDTO updatedHotelDTO = await _hotel.Update(hotelDTO);
            return Ok(updatedHotelDTO);
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
