using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Schedule;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for performing CRUD operations on Schedule table. ")]
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
        
        [SwaggerOperation(Description = " This route is for adding a round(schedule). Only admins can access these routes. ")]
        [Authorize(Roles = "Admin, Core, Editor ")]
        [HttpPost]   
        public async Task<ActionResult<Schedule>> AddSchedule(DataForScheduleDto data)
        {    
            return Ok(await _repo.AddSchedule(data));
        }
        
        [SwaggerOperation(Description = " This route is for modifying the schedule. Only admins can access these routes.")]
        [Authorize(Roles = "Admin, Core, Editor ")]
        [HttpPut]   
        public async Task<ActionResult<Schedule>> UpdateSchedule(DataForScheduleDto dataFromClient)
        {    
            return Ok(await _repo.UpdateSchedule(dataFromClient));
        }
        
        [SwaggerOperation(Description = " This route is for deleting the schedule. Only admins can access these routes. ")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]   
        public async Task<ActionResult<Schedule>> RemoveSchedule(DataForDeletingScheduleDto dataFromClient)
        {    
            return Ok(await _repo.RemoveSchedule(dataFromClient));
        }
    }
}