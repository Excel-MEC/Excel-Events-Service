using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models.Custom;
using API.Services.Interfaces;
using AutoMapper;
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddEvent([FromForm] DataForAddingEventDto eventDataFromClient)
        {
            var success = await _repo.AddEvent(eventDataFromClient);
            if (success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Something went wrong");
        }
       
        [SwaggerOperation(Description = "This route is for updating event details. Only admins can access this route.")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdateEvent([FromForm] DataForUpdatingEventDto eventDataFromClient)
        {
            var success = await _repo.UpdateEvent(eventDataFromClient);
            if (success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Something went wrong");
        }

        [SwaggerOperation(Description = "This route is for deleting an event. Only admins can access this route.")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]    
        public async Task<ActionResult<OkResponse>> DeleteEvent(DataForDeletingEventDto dataForDeletingEvent)
        {
            var success = await _repo.DeleteEvent(dataForDeletingEvent);
            if (success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Error Deleting Event");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventForDetailedViewDto>> GetEvent(int id)
        {
            var eventFromRepo = await _repo.GetEvent(id);
            return Ok(eventFromRepo);
        }
        
        [SwaggerOperation(Description = " This route returns all the events that matches the applied filters. ")]
        [HttpGet("event_type={eventType}&category={category}")]
        public async Task<ActionResult> FilteredList(string eventType, string category)
        {
            int eventTypeId, categoryId;
            eventTypeId = Array.IndexOf(Constants.EventType, eventType);
            categoryId = Array.IndexOf(Constants.Category, category);
            var filteredEvents = await _repo.FilteredList(eventTypeId, categoryId);
            return Ok(filteredEvents);
        }

        [SwaggerOperation(Description = " This route returns all the events that matches the given event type.")]
        [HttpGet("type/{event_type}")]
        public async Task<ActionResult> GetEventsOfType(string event_type)
        {
            var eventTypeId = Array.IndexOf(Constants.EventType, event_type);
            var filteredEvents = await _repo.EventListOfType(eventTypeId);
            return Ok(filteredEvents);
        }

        [SwaggerOperation(Description = "This route returns all the events that matches the given event category. ")]
        [HttpGet("category/{category}")]
        public async Task<ActionResult> GetEventsOfCategory(string category)
        {
            var categoryId = Array.IndexOf(Constants.EventType, category);
            var filteredEvents = await _repo.EventListOfCategory(categoryId);
            return Ok(filteredEvents);
        }
    }
}