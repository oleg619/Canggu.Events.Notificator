using AutoMapper;
using CangguEvents.Domain;
using CangguEvents.MongoDb.Entities;

namespace CangguEvents.MongoDb
{
    public class MongoProfile : Profile
    {
        public MongoProfile()
        {
            CreateMap<Location, LocationEntity>().ReverseMap();
            CreateMap<EventInfo, EventEntity>().ReverseMap();
        }
    }
}