using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<List<HotelRoomDTO>> GetHotelRooms(IRoom room, IAmenity amenity);

        Task<List<HotelRoomDTO>> GetHotelRoomsForHotel(int hotelId, IRoom room, IAmenity amenity);

        Task<HotelRoomDTO> GetRoomDetails(int hotelId, int roomNumber, IRoom room, IAmenity amenity);

        Task<HotelRoomDTO> GetHotelRoomWithoutDetails(int hotelId, int roomNumber);

        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoomDTO, int hotelId);

        Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoomDTO);

        Task Delete(int hotelId, int roomNumber);
    }
}
