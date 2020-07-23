using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Schedule;
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
    }
}