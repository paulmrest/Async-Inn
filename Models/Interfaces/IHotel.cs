using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<List<HotelDTO>> GetHotels(IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        Task<HotelDTO> GetHotel(int id, IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        Task<HotelDTO> GetHotelByName(string hotelName, IHotelRoom hotelRoom, IRoom room, IAmenity amenity);

        Task<HotelDTO> Create(HotelDTO hotelDTO);

        Task<HotelDTO> Update(HotelDTO hotelDTO);

        Task Delete(int id);
    }
}
