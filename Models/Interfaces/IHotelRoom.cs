using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task<List<HotelRoom>> GetHotelRooms();

        Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber);

        Task<HotelRoom> Create(HotelRoom hotelRoom);

        Task<HotelRoom> Update(HotelRoom hotelRoom);

        Task Delete(int hotelId, int roomNumber);
    }
}
