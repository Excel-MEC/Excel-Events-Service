using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The Routes under this controller need authorization.")]
    [Route("/Result")]
    [ApiController]
    public class ResultController: ControllerBase
    {
        private readonly IResultRepository _repo;

        public ResultController(IResultRepository repo)
        {
            _repo = repo;
        }
        

        [SwaggerOperation(Description = "For adding result of an event.")]
        [Authorize(Roles = "Admin,Core,Editor")]
        [HttpPost]
        public async Task<ActionResult<Result>> AddEventResult(DataForAddingResultDto dataForAddingResult)
        {
            var newResult = await _repo.AddEventResult(dataForAddingResult);
            return Ok(newResult);
        }

        [SwaggerOperation(Description = "For updating result of an event.")]
        [Authorize(Roles = "Admin,Core,Editor")]
        [HttpPut]
        public async Task<ActionResult<Result>> AddEventResult(DataForUpdatingResultDto dataForUpdatingResult)
        {
            var modifiedResult = await _repo.UpdateEventResult(dataForUpdatingResult);
            return Ok(modifiedResult);
        }

        [SwaggerOperation(Description = "For deleting a result of an event.")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        public async Task<ActionResult<Result>> RemoveResult(DataForDeletingResultDto dataForDeletingResult)
        {
            var deletedResult = await _repo.RemoveResult(dataForDeletingResult.Id);
            return Ok(deletedResult);
        }

        [SwaggerOperation(Description = "For retrieving Result of a particular event")]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<List<ResultForListViewDto>>> EventResults(int eventId)
        {
            var result = await _repo.GetEventResults(eventId);
            return Ok(result);
        }

        [SwaggerOperation(Description = "For deleting all results of an event.")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete("event/{eventId}")]
        public async Task<ActionResult<List<Result>>> RemoveAllResult(int eventId)
        {
            var deletedResults = await _repo.RemoveAllResults(eventId);
            return Ok(deletedResults);
        } 
        
    }
}
