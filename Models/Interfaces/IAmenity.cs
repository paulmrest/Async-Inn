using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<List<Amenity>> GetAmenities();

        Task<Amenity> GetAmenity(int id);

        Task<Amenity> Create(Amenity amenity);

        Task<Amenity> Update(Amenity amenity);

        Task Delete(int id);
    }
}
