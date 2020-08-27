using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.EventHeads;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for performing CRUD operations on Schedule table. ")]
    [Route("/eventhead")]
    [ApiController]
    public class EventHeadController : Controller
    {
        private readonly IEventHeadRepository _repo;

        public EventHeadController(IEventHeadRepository repo)
        {
            _repo = repo;
        }
        
        [SwaggerOperation(Description = " This route is for returning all the EventHeads. Only admins can access these routes. ")]
        [Authorize(Roles = "Admin")]
        [HttpGet]   
        public async Task<List<EventHeadForViewDto>> EventHeadList()
        {
            var eventHeads =  await _repo.ListEventHeads();
            return eventHeads;
        }
        
        [SwaggerOperation(Description = " This route is for returning the details of an EventHead ")]
        [Authorize(Roles = "Admin, Core, Editor")]
        [HttpGet("{id}")]   
        public async Task<EventHeadForViewDto> GetEventHead(int id)
        {
            var eventHead =  await _repo.GetEventHead(id);
            return eventHead;
        }
        
        [SwaggerOperation(Description = " This route is for adding new Event Head. Only admins can access these routes. ")]
        [Authorize(Roles = "Admin, Core, Editor")]
        [HttpPost]   
        public async Task<ActionResult<EventHead>> AddEventHead(DataForAddingEventHead newEventHead)
        {    
           return Ok(await _repo.AddEventHead(newEventHead));
        }
        
        [SwaggerOperation(Description = " This route is for updating the details of an Event Head. Only admins can access these routes.")]
        [Authorize(Roles = "Admin, Core, Editor")]
        [HttpPut]   
        public async Task<ActionResult<EventHead>> UpdateEventHead(DataForUpdatingEventHeadDto dataFromClient)
        {    
           return Ok(await _repo.UpdateEventHead(dataFromClient));
        }
        
        [SwaggerOperation(Description = " This route is for deleting an Event Head. Only admins can access these routes. ")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]   
        public async Task<ActionResult<EventHead>> RemoveEventHead(DataForDeletingEventHeadDto dataFromClient)
        {    
            return Ok(await _repo.DeleteEventHead(dataFromClient));
        }
    }
}