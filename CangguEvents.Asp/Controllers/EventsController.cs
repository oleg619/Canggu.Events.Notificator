using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace CangguEvents.Asp.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventsController(
            IEventsRepository eventsRepository,
            IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var allEvents = await _eventsRepository.GetAllEvents();

            var eventDtos = _mapper.Map<List<EventDto>>(allEvents);

            return Ok(eventDtos);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateEventDto createEventDto)
        {
            var eventInfo = _mapper.Map<EventInfo>(createEventDto);
            var addEvent = await _eventsRepository.AddEvent(eventInfo);
            var eventDto = _mapper.Map<EventDto>(addEvent);

            return Ok(eventDto);
        }
    }

    class EventDto : CreateEventDto
    {
        public int Id { get; set; }

        public EventDto(byte[] image, string description, string name, LocationDto location, List<DayOfWeek> dayOfWeeks,
            int id) : base(description, name, location, image, dayOfWeeks)
        {
            Id = id;
        }
    }

    public class CreateEventDto
    {
        public CreateEventDto(string description, string name, LocationDto location, byte[] image,
            List<DayOfWeek> dayOfWeeks)
        {
            Description = description;
            Name = name;
            Location = location;
            Image = image;
            DayOfWeeks = dayOfWeeks;
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LocationDto Location { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public List<DayOfWeek> DayOfWeeks { get; }
    }

    public class LocationDto
    {
        public LocationDto(string googleUrl, float latitude, float longitude)
        {
            GoogleUrl = googleUrl;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string GoogleUrl { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}