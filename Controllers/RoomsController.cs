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

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;
        private readonly IAmenity _amenity;

        /// <summary>
        /// Instantiates a new RoomsController object.
        /// </summary>
        /// <param name="hotel">
        /// IRoom: a repository object that implements the IRoom interface
        /// </param>
        public RoomsController(IRoom room, IAmenity amenity)
        {
            _room = room;
            _amenity = amenity;
        }

        // GET: /api/Rooms
        [HttpGet("/api/Rooms")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms(_amenity);
        }

        // GET: /api/Rooms/{id}
        [HttpGet("/api/Rooms/{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            return await _room.GetRoom(id, _amenity);
        }

        // POST: /api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("/api/Rooms")]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO roomDTO)
        {
            await _room.Create(roomDTO);
            return CreatedAtAction("GetRoom", new { id = roomDTO.Id }, roomDTO);
        }

        // PUT: /api/Rooms/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Rooms/{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomDTO)
        {
            if (id != roomDTO.Id)
            {
                return BadRequest();
            }
            var updatedRoom = await _room.Update(roomDTO);
            return Ok(updatedRoom);
        }

        // POST: /api/Rooms/{roomId}/Amenity/{amenityId}
        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> AddRoomAmenity(int roomId, int amenityId)
        {
            await _room.AddAmenityToRoom(amenityId, roomId);
            return Ok();
        }

        // DELETE: /api/Rooms/{roomId}/Amenity/{amenityId}
        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            await _room.RemoveAmenityFromRoom(amenityId, roomId);
            return Ok();
        }

        // DELETE: /api/Rooms/{id}
        [HttpDelete("/api/Rooms/{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }
    }
}
