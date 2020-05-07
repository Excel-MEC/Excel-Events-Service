using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using API.Models.Custom;
using API.Services.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IEventService _service;

        public EventController(IEventRepository repo, IMapper mapper, IEventService service)
        {
            _repo = repo;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventForListViewDto>>> Get()
        {
            List<EventForListViewDto> events = await _repo.EventList();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            Event eventFromRepo = await _repo.GetEvent(id);
            return Ok(eventFromRepo);
        }

        [SwaggerOperation(Description = "This route is for Adding new Events")]
        [HttpPost("add")]
        public async Task<ActionResult> AddEvent([FromForm] DataForAddingEventDto eventDataFromClient)
        {
            bool success = await _repo.AddEvent(eventDataFromClient);
            if (success)
                return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Something went wrong");
        }

        [SwaggerOperation(Description = "This route is for Adding new Events")]
        [HttpPost("update")]
        public async Task<ActionResult> UpdateEvent([FromForm] DataForUpdatingEventDto eventDataFromClient)
        {
            bool success = await _repo.UpdateEvent(eventDataFromClient);
            if (success)
                return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Something went wrong");
        }
    }
}