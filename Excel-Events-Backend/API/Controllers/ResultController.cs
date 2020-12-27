using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Data.Interfaces;
using API.Dtos;
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
    [Route("/Result")]
    [ApiController]
    public class ResultController: ControllerBase
    {
        private readonly IResultRepository _repo;

        public ResultController(IResultRepository repo)
        {
            _repo = repo;
        }
        
       
        [SwaggerOperation(Description = "For retrieving all Results")]
        [HttpGet]
        public async Task<ActionResult<List<Result>>> AllResults()
        {
            var results = await _repo.AllResults();
            return Ok(results);
        }

        [SwaggerOperation(Description = "For adding result of an event.")]
        [HttpPost]
        public async Task<ActionResult<Result>> AddEventResult(DataForAddingResultDto dataForAddingResult)
        {
            var newResult = await _repo.AddEventResult(dataForAddingResult);
            return Ok(newResult);
        }

        [SwaggerOperation(Description = "For updating result of an event.")]
        [HttpPut]
        public async Task<ActionResult<Result>> AddEventResult(DataForUpdatingResultDto dataForUpdatingResult)
        {
            var modifiedResult = await _repo.UpdateEventResult(dataForUpdatingResult);
            return Ok(modifiedResult);
        }
        
        
        [SwaggerOperation(Description = "For retrieving Result of a particular event")]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<ResultForViewDto>>> EventResults(int eventId)
        {
            var result = await _repo.GetEventResults(eventId);
            return Ok(result);
        }
    }
}
