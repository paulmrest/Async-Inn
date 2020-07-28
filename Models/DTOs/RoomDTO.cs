﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Layout { get; set; }

        public List<AmenityDTO> Amenities { get; set; }
    }
}
