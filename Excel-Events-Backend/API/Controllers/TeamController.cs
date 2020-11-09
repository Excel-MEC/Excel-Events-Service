using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Data.Interfaces;
using API.Dtos.Default;
using API.Dtos.Teams;
using API.Models;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The Routes under this controller need authorization.")]
    [Authorize]
    [Route("/Team")]
    [ApiController]
    public class TeamController: ControllerBase
    {
        private readonly ITeamRepository _repo;

        public TeamController(ITeamRepository repo)
        {
            _repo = repo;
        }
        
       
        [SwaggerOperation(Description = "For finding all Teams")]
        [HttpGet]
        public async Task<ActionResult<List<Team>>> AllTeams()
        {
            var teams = await _repo.AllTeams();
            return Ok(teams);
        }
        
        [SwaggerOperation(Description = "For Creating new Team for registering team events.")]
        [HttpPost]
        public async Task<ActionResult<Team>> CreateNewTeam(DataForAddingTeamDto dataForAddingTeam)
        {
            var newTeam = await _repo.CreateTeam(dataForAddingTeam);
            return Ok(newTeam);
        }
         
        [SwaggerOperation(Description = "For finding Team with corresponding Id.")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> CreateNewTeam(int id)
        {
            var team = await _repo.FindTeam(id);
            return Ok(team);
        }
        
        
        [SwaggerOperation(Description = "For finding Teams of particular event")]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<Team>>> EventTeams(int eventId)
        {
            var teams = await _repo.FindEventTeams(eventId);
            return Ok(teams);
        }
    }
}
