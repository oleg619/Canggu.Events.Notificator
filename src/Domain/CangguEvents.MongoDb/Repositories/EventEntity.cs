using System;
using System.Collections.Generic;
using AutoMapper;
using CangguEvents.Domain;

namespace CangguEvents.MongoDb.Repositories
{
    class MongoProfile : Profile
    {
        public MongoProfile()
        {
            CreateMap<Location, LocationEntity>().ReverseMap();
            CreateMap<EventInfo, EventEntity>().ReverseMap();
        }
    }


    public class LocationEntity
    {
        public string GoogleUrl { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public LocationEntity(string googleUrl, float latitude, float longitude)
        {
            GoogleUrl = googleUrl;
            Latitude = latitude;
            Longitude = longitude;
        }
    }

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