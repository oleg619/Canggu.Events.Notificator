using System;
using System.Collections.Generic;

namespace CangguEvents.MongoDb.Entities
{
    public class EventEntity
    {
        public long Id { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public LocationEntity Location { get; set; }

        public List<DayOfWeek> DayOfWeeks { get; set; }
    }
}