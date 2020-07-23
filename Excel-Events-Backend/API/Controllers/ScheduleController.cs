using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Schedule;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for performing CRUD operations on Schedules table.")]
    [Route("/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository _repo;
        public ScheduleController(IScheduleRepository repo)
        {
            _repo = repo;
        }

        [SwaggerOperation(Description = " This route is for returning all the events in the scheduled order. ")]
        [HttpGet]   
        public async Task<List<EventForScheduleListViewDto>> EventList()
        {
            var events =  await _repo.ScheduleList();
            return events;
        }
        
        [SwaggerOperation(Description = " This route is for adding new round. ")]
        [Authorize(Roles = "Admin")]
        [HttpPost]   
        public async Task<ActionResult> AddRound(DataForAddingEventRoundDto data)
        {    
            var success =  await _repo.AddRound(data);
            if(success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Problem in adding new round.");
        }

    }
}