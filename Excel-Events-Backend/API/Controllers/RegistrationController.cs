using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Registration;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag(" The routes under this controller are for performing CRUD operations on Registrations table. ")]
    [Authorize]
    [Route("/registration")]    
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _repo;
        public RegistrationController(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        [SwaggerOperation(Description = " This route is used to provide event registration. ")]
        [HttpPost]
        public async Task<ActionResult> Register(DataFromClientDto data)
        {
            int excelId = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
            var success = await _repo.Register(excelId, data.Id);
            if(success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Problem registering user");
        }

        [SwaggerOperation(Description = " This route is used to clear a user's data from registration table when the user account is deleted. Only admins can access this route. ")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> ClearUserData(DataFromClientDto data)
        {
            var success = await _repo.ClearUserData(data.Id);
            if(success) return Ok(new OkResponse { Response = "Success"});
            throw new Exception("Problem clearing user data. Check out the userid");
        }

        [SwaggerOperation(Description = " This route is to return a list of events registered by a user. ")]
        [HttpGet]
        public async Task<ActionResult<List<EventForListViewDto>>> EventList()
        {
            int id = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            var list = await _repo.EventList(id);
            return list;
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Description = " This route is used to return a list of users,registered for an event. Only admins can access this route. ")]
        [HttpGet("{eventId}/users")]
        public async Task<ActionResult<List<int>>> UserList(string eventId)
        {
            var list = await _repo.UserList(int.Parse(eventId));
            return list;
        }

        [SwaggerOperation(Description = " This route is used to check whether a user has registered for an event or not. ")]
        [HttpGet("{eventId}")]
        public async Task<ActionResult<bool>> HasRegistered(string eventId)
        {
            int excelId = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            var success = await _repo.HasRegistered(excelId, int.Parse(eventId));
            return Ok(success);
        }

    }
}