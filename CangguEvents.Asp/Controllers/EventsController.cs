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
    }

    public class CreateEventDto
    {
        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        public string GoogleUrl { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}