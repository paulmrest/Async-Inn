using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;

        /// <summary>
        /// Instantiates a new HotelRepository object.
        /// </summary>
        /// <param name="context">
        /// AsyncInnDbContext: an object that inherits from DbContext
        /// </param>
        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Hotels.
        /// </summary>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<List<HotelDTO>>: a list of HotelDTO objects embedded in a Task object
        /// </returns>
        public async Task<List<HotelDTO>> GetHotels(IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            List<HotelDTO> hotelDTOs = await _context.Hotels
                .Select(x =>
                    new HotelDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StreetAddress = x.StreetAddress,
                        City = x.City,
                        State = x.State,
                        Phone = x.Phone
                    }
                )
                .ToListAsync();
            foreach (HotelDTO oneHotelDTO in hotelDTOs)
            {
                oneHotelDTO.Rooms = await hotelRoom.GetHotelRoomsForHotel(oneHotelDTO.Id, room, amenity);
            }
            return hotelDTOs;
        }

        /// <summary>
        /// Gets a Hotel by id.
        /// </summary>
        /// <param name="id">
        /// int: a Hotel id
        /// </param>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: a HotelDTO object embedded in a Task object
        /// </returns>
        public async Task<HotelDTO> GetHotel(int id, IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            HotelDTO hotelDTO = await _context.Hotels
                .Where(x => x.Id == id)
                .Select(x =>
                    new HotelDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StreetAddress = x.StreetAddress,
                        City = x.City,
                        State = x.State,
                        Phone = x.Phone
                    }
                )
                .FirstAsync();
            hotelDTO.Rooms = await hotelRoom.GetHotelRoomsForHotel(id, room, amenity);
            return hotelDTO;
        }

        /// <summary>
        /// Gets a Hotel by name.
        /// </summary>
        /// <param name="hotelName">
        /// string: a hotel name
        /// </param>
        /// <param name="hotelRoom">
        /// IHotelRoom: an object that implements the IHotelRoom interface
        /// </param>
        /// <param name="room">
        /// IRoom: an object that implements the IRoom interface
        /// </param>
        /// <param name="amenity">
        /// IAmenity: an object that implements the IAmenity Interface
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: a HotelDTO object embedded in a Task object
        /// </returns>
        public async Task<HotelDTO> GetHotelByName(string hotelName, IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            int hotelId = await _context.Hotels.Where(x => x.Name == hotelName)
                                               .Select(x => x.Id)
                                               .FirstAsync();
            return await GetHotel(hotelId, hotelRoom, room, amenity);
        }

        /// <summary>
        /// Saves a Hotel object to the database.
        /// </summary>
        /// <param name="hotel">
        /// HotelDTO: a HotelDTO object
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: the parameter HotelDTO object after being saved to the database
        /// </returns>
        public async Task<HotelDTO> Create(HotelDTO hotelDTO)
        {
            Hotel hotelEntity = new Hotel()
            {
                Name = hotelDTO.Name,
                StreetAddress = hotelDTO.StreetAddress,
                City = hotelDTO.City,
                State = hotelDTO.State,
                Phone = hotelDTO.Phone
            };
            _context.Entry(hotelEntity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            hotelDTO.Id = hotelEntity.Id;
            return hotelDTO;
        }

        /// <summary>
        /// Updates a Hotel object in the database.
        /// </summary>
        /// <param name="hotel">
        /// HotelDTO: a HotelDTO object with updated information
        /// </param>
        /// <returns>
        /// Task<HotelDTO>: the parameter HotelDTO object after being updated
        /// </returns>
        public async Task<HotelDTO> Update(HotelDTO hotelDTO)
        {
            Hotel hotelEntity = new Hotel()
            {
                Id = hotelDTO.Id,
                Name = hotelDTO.Name,
                StreetAddress = hotelDTO.StreetAddress,
                City = hotelDTO.City,
                State = hotelDTO.State,
                Phone = hotelDTO.Phone
            };
            _context.Entry(hotelEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelDTO;
        }

        /// <summary>
        /// Deletes a Hotel object from the database.
        /// </summary>
        /// <param name="id">
        /// int: the id of the Hotel to be deleted
        /// </param>
        /// <returns>
        /// Task: an empty Task object
        /// </returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}