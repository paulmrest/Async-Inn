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
using Microsoft.VisualBasic;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        //private readonly AsyncInnDbContext _context;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }

        //GET: api/Hotel/{hotelId}/Rooms
        [HttpGet("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetRoomsForHotel(int hotelId)
        {
            return await _hotelRoom.GetHotelRoomsForHotel(hotelId);
        }

        //GET: api/Hotel/{hotelId}/Rooms
        [HttpGet("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetRoomDetails(int hotelId, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomsForHotel(hotelId);
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(HotelRoom hotelRoom, int hotelId, int roomNumber)
        {
            if (hotelId != hotelRoom.HotelId || roomNumber != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }
            var updatedHotelRoom = await _hotelRoom.Update(hotelRoom);
            return Ok(updatedHotelRoom);
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Hotels/{hotelId}/Rooms/")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom, int hotelId)
        {
            hotelRoom.HotelId = hotelId;
            await _hotelRoom.Create(hotelRoom);
            return CreatedAtAction("GetHotelRoom", new { hotelId = hotelRoom.HotelId, roomId = hotelRoom.RoomId }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);
            return NoContent();
        }
    }
}