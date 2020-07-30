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
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;
        private readonly IRoom _room;
        private readonly IAmenity _amenity;

        /// <summary>
        /// Instantiates a new HotelRoomsController object.
        /// </summary>
        /// <param name="hotelRoom">
        /// IHotelRoom: a repository object that implements the IHotelRoom interface
        /// </param>
        public HotelRoomsController(IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            _hotelRoom = hotelRoom;
            _room = room;
            _amenity = amenity;
        }

        // GET: api/HotelRooms
        [HttpGet("/api/Hotels/Rooms/")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms(_room, _amenity);
        }

        //GET: /api/Hotel/{hotelId}/Rooms
        [HttpGet("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetRoomsForHotel(int hotelId)
        {
            return await _hotelRoom.GetHotelRoomsForHotel(hotelId, _room, _amenity);
        }

        //GET: /api/Hotels/{hotelId}/Rooms/{hotelNumber}
        [HttpGet("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetRoomDetails(int hotelId, int roomNumber)
        {
            return await _hotelRoom.GetRoomDetails(hotelId, roomNumber, _room, _amenity);
        }

        //GET: /api/Hotels/{hotelId}/Rooms/{hotelNumber}
        [HttpGet("/api/Hotels/{hotelId}/Rooms/NoDetails/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetRoomWithoutDetails(int hotelId, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomWithoutDetails(hotelId, roomNumber);
        }

        // POST: /api/Hotels/{hotelId}/Rooms/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Hotels/{hotelId}/Rooms/")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoomDTO hotelRoomDTO, int hotelId)
        {
            await _hotelRoom.Create(hotelRoomDTO, hotelId);
            return CreatedAtAction("GetHotelRoom", new { hotelId = hotelRoomDTO.HotelId, roomId = hotelRoomDTO.RoomId }, hotelRoomDTO);
        }

        // PUT: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(HotelRoomDTO hotelRoomDTO, int hotelId, int roomNumber)
        {
            if (hotelId != hotelRoomDTO.HotelId || roomNumber != hotelRoomDTO.RoomNumber)
            {
                return BadRequest();
            }
            var updatedHotelRoom = await _hotelRoom.Update(hotelRoomDTO);
            return Ok(updatedHotelRoom);
        }

        // DELETE: /api/Hotels/{hotelId}/Rooms/{roomNumber}
        [HttpDelete("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);
            return NoContent();
        }
    }
}