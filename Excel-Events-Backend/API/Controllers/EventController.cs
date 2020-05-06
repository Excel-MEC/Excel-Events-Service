using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for perfoming CRUD optrations on Events table.")]
    [Route("/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repo;

        public EventController(IEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventForListViewDto>>> Get()
        {
            List<EventForListViewDto> events = await _repo.EventList();
            return Ok(events);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Event>> GetEvent(int Id)
        {
            Event eventFromRepo = await _repo.GetEvent(Id);
            return Ok(eventFromRepo);
        }
    }
}