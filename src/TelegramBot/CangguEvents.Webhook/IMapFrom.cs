using AutoMapper;
using CangguEvents.Asp.Controllers;

namespace CangguEvents.Asp
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}