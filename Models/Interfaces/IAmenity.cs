using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models.DTOs;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenity
    {
        Task<List<AmenityDTO>> GetAmenities();

        Task<AmenityDTO> GetAmenity(int id);

        Task<AmenityDTO> Create(AmenityDTO amenity);

        Task<AmenityDTO> Update(AmenityDTO amenityDTO);

        Task Delete(int id);
    }
}
