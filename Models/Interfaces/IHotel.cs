using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<List<Hotel>> GetHotels();

        Task<Hotel> GetHotel(int id);

        Task<Hotel> Create(Hotel hotel);

        Task<Hotel> Update(Hotel hotel);

        Task Delete(int id);
    }
}
