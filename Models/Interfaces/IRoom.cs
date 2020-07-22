using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<List<Room>> GetRooms();

        Task<Room> GetRoom(int id);

        Task<Room> Create(Room room);

        Task<Room> Update(Room room);

        Task Delete(int id);
    }
}
