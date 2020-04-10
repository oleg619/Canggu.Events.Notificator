using System;
using System.Collections.Generic;

namespace CangguEvents.Domain
{
    public readonly struct EventInfo : IEquatable<EventInfo>
    {
        public long Id { get; }
        public byte[] Image { get; }
        public string Description { get; }
        public string Name { get; }
        public Location Location { get; }
        public List<DayOfWeek> DayOfWeeks { get; }

        public EventInfo(byte[] image, string description, string name, Location location, List<DayOfWeek> dayOfWeeks,
            long id)
        {
            Image = image;
            Location = location;
            Description = description;
            Name = name;
            DayOfWeeks = dayOfWeeks;
            Id = id;
        }

        public bool Equals(EventInfo other) =>
            Id == other.Id && Equals(Image, other.Image) && Description == other.Description &&
            Name == other.Name && Equals(Location, other.Location) && Equals(DayOfWeeks, other.DayOfWeeks);

        public override bool Equals(object obj) => obj is EventInfo other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Id, Image, Description, Name, Location, DayOfWeeks);
    }
}