using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Registration;
using API.Models;
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

        [SwaggerOperation(Description = " This route is to return a list of events registered by a user. ")]
        [HttpGet]
        public async Task<ActionResult<List<EventForListViewDto>>> EventList()
        {
            var id = int.Parse(User.Claims.First(x => x.Type == "user_id").Value);
            return Ok(await _repo.EventList(id));
        }
        
        [SwaggerOperation(Description = " This route is used to provide event registration. ")]
        [HttpPost]
        public async Task<ActionResult<RegistrationForViewDto>> AddRegistration(DataForRegistrationDto dataForRegistration)
        {
            var excelId = int.Parse(User.Claims.First(i => i.Type == "user_id").Value);
            return Ok(await _repo.Register(excelId, dataForRegistration));
        }

        [SwaggerOperation(Description = " This route is used to clear a user's data from registration table when the user account is deleted. Only admins can access this route. ")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        public async Task<ActionResult<List<RegistrationForViewDto>>> ClearUserData(DataForClearingUserRegistrationDto dataForClearingUserRegistration)
        {
            return Ok(await _repo.ClearUserData(dataForClearingUserRegistration));
        }

        [SwaggerOperation(Description =
            " This route is used to check whether a user has registered for an event or not. ")]
        [HttpGet("{eventId}")]
        public async Task<ActionResult<bool>> HasRegistered(string eventId)
        {
            var excelId = int.Parse(User.Claims.First(x => x.Type == "user_id").Value);
            return Ok(await _repo.HasRegistered(excelId, int.Parse(eventId)));
        }

        [SwaggerOperation(Description =
            " This route is used to change the registered team.")]
        [HttpPut("team")]
        public async Task<ActionResult<bool>> ChangeTeam(DataForRegistrationDto dataForRegistration)
        {
            var excelId = int.Parse(User.Claims.First(x => x.Type == "user_id").Value);
            return Ok(await _repo.ChangeTeam(excelId, dataForRegistration));
        }

        [Authorize(Roles = "Admin, Core, Editor")]
        [SwaggerOperation(Description = " This route is used to provide event registration by Admin, Core or Editor. ")]
        [HttpPost("admin")]
        public async Task<ActionResult<RegistrationForViewDto>> AddRegistrationByAdmin(DataForRegistrationByAdminDto data)
        {
            return Ok(await _repo.Register(data.ExcelId, data.RegistrationData));
        }
        
        [Authorize(Roles = "Admin, Core, Editor")]
        [SwaggerOperation(Description = " This route is delete an user registration by Admin, Core or Editor. ")]
        [HttpDelete("admin")]
        public async Task<ActionResult<RegistrationForViewDto>> RemoveRegistration(DataForRemovingRegistrationByAdminDto dataForRemovingRegistrationByAdmin)
        {
            return Ok(await _repo.RemoveRegistration(dataForRemovingRegistrationByAdmin.ExcelId, dataForRemovingRegistrationByAdmin.EventId));
        }
        
        [Authorize(Roles = "Admin, Core, Editor, Staff")]
        [SwaggerOperation(Description = " This route is used to return a list of userIds of the users who registered for an event. Only admins can access this route. ")]
        [HttpGet("{eventId}/users")]
        public async Task<ActionResult<List<UserForViewDto>>> UserList(string eventId)
        {
            return Ok( await _repo.UserList(int.Parse(eventId)));
        }
        
        
    }
}