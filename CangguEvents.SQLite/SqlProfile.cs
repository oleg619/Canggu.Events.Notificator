using System;
using AutoMapper;
using CangguEvents.SQLite.Entities;
using Shared;

namespace CangguEvents.SQLite
{
    public class SqlProfile : Profile
    {
        public SqlProfile()
        {
            CreateMap<EventInfo, EventEntity>().ReverseMap();
            CreateMap<Location, LocationEntity>().ReverseMap();
            CreateMap<DayOfWeek, DayOfWeek>().ReverseMap();
            CreateMap<DayOfWeek, DayOfWeekEntity>().ForMember(entity => entity.DayOfWeek,
                opt => opt.MapFrom(dayOfWeek => dayOfWeek)).ReverseMap();
        }
    }
}