using AutoMapper;
using CangguEvents.Asp.Controllers;
using Shared;

namespace CangguEvents.Asp
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<CreateEventDto, EventInfo>().ReverseMap();
            CreateMap<EventInfo, EventDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}