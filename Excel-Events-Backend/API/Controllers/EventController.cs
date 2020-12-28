using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for performing CRUD operations on Events table.")]
    [Route("/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repo;

        public EventController(IEventRepository repo)
        {
            _repo = repo;
        }

        [SwaggerOperation(Description = " This route returns a list of all the events. ")]
        [HttpGet]
        public async Task<ActionResult<List<EventForListViewDto>>> Get()
        {
            var events = await _repo.EventList();
            return Ok(events);
        }

        [SwaggerOperation(Description = " This route is for adding new events. Only admins can access this route. ")]
        [Authorize(Roles = "Admin,Core,Editor")]
        [HttpPost]
        public async Task<ActionResult<Event>> AddEvent([FromForm] DataForAddingEventDto eventDataFromClient)
        {
            return Ok(await _repo.AddEvent(eventDataFromClient));
        }
       
        [SwaggerOperation(Description = "This route is for updating event details. Only admins can access this route.")]
        [Authorize(Roles = "Admin, Core, Editor")]
        [HttpPut]
        public async Task<ActionResult<Event>> UpdateEvent([FromForm] DataForUpdatingEventDto eventDataFromClient)
        {
            return Ok(await _repo.UpdateEvent(eventDataFromClient));
        }

        [SwaggerOperation(Description = "This route is for deleting an event. Only admins can access this route.")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]    
        public async Task<ActionResult<Event>> DeleteEvent(DataForDeletingEventDto dataForDeletingEvent)
        {
           return Ok(await _repo.DeleteEvent(dataForDeletingEvent));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventForDetailedViewDto>> GetEvent(int id)
        {
            int? excelId = null;
            if (User.Identity.IsAuthenticated)
            {
                excelId = int.Parse(User.Claims.First(x => x.Type == "user_id").Value);
            }
            var eventFromRepo = await _repo.GetEvent(id, excelId);
            return Ok(eventFromRepo);
        }
        
        [SwaggerOperation(Description = " This route returns all the events that matches the applied filters. ")]
        [HttpGet("event_type={eventType}&category={category}")]
        public async Task<ActionResult<List<EventForListViewDto>>> FilteredList(string eventType, string category)
        {
            int eventTypeId, categoryId;
            eventTypeId = Array.IndexOf(Constants.EventType, eventType);
            categoryId = Array.IndexOf(Constants.Category, category);
            var filteredEvents = await _repo.FilteredList(eventTypeId, categoryId);
            return Ok(filteredEvents);
        }

        [SwaggerOperation(Description = " This route returns all the events that matches the given event type.")]
        [HttpGet("type/{event_type}")]
        public async Task<ActionResult<List<EventForListViewDto>>> GetEventsOfType(string event_type)
        {
            var eventTypeId = Array.IndexOf(Constants.EventType, event_type);
            var filteredEvents = await _repo.EventListOfType(eventTypeId);
            return Ok(filteredEvents);
        }

        [SwaggerOperation(Description = "This route returns all the events that matches the given event category. ")]
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<EventForListViewDto>>> GetEventsOfCategory(string category)
        {
            var categoryId = Array.IndexOf(Constants.EventType, category);
            var filteredEvents = await _repo.EventListOfCategory(categoryId);
            return Ok(filteredEvents);
        }

        [SwaggerOperation(Description = " This route returns a list of all the events with results ")]
        [HttpGet("results")]
        public async Task<ActionResult<List<EventWithResultDto>>> GetEventswithResults()
        {
            var events = await _repo.GetEventsWithResults();
            return Ok(events);
        }
    }
}