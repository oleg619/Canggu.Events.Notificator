using System;
using System.Collections.Generic;

namespace Shared
{
    public readonly struct EventInfo
    {
        public int Id { get; }
        public byte[] Image { get; }
        public string Description { get; }
        public string Name { get; }
        public Location Location { get; }
        public List<DayOfWeek> DayOfWeeks { get; }

        public EventInfo(byte[] image, string description, string name, Location location, List<DayOfWeek> dayOfWeeks,
            int id)
        {
            Image = image;
            Location = location;
            Description = description;
            Name = name;
            DayOfWeeks = dayOfWeeks;
            Id = id;
        }
    }
}