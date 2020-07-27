using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<List<HotelRoom>> GetHotelRooms();

        Task<HotelRoom> GetHotelRoomWithoutDetails(int hotelId, int roomNumber);

        Task<List<HotelRoom>> GetHotelRoomsForHotel(int hotelId);

        Task<HotelRoom> GetRoomDetails(int hotelId, int roomNumber);

        Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId);

        Task<HotelRoom> Update(HotelRoom hotelRoom);

        Task Delete(int hotelId, int roomNumber);
    }
}
