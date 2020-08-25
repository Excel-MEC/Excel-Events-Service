using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Highlight;
using API.Extensions.CustomExceptions;
using API.Models;
using API.Models.Custom;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag(" The routes under this controller are for performing CRUD operations on Highlights table. ")]
    [Route("/highlights")]
    [ApiController]
    public class HighlightController : ControllerBase
    {
        private readonly IHighlightRepository _repo;

        public HighlightController(IHighlightRepository repo)
        {
            _repo = repo;
        }

        [SwaggerOperation(Description = " This route is for listing the event highlights. ")]
        [HttpGet]
        public async Task<ActionResult<List<Highlight>>> Get()
        {
            var events = await _repo.GetHighlights();
            return Ok(events);
        }
        
        [SwaggerOperation(Description = " This route is for adding an event highlight. Only admins can access this route. ")]
        [Authorize(Roles = "Admin, Core, Editor")]
        [HttpPost]
        public async Task<ActionResult<OkResponse>> Add([FromForm] DataForAddingHighlightDto dataForAddingHighlight)
        {
            if( dataForAddingHighlight.Name == null ) throw new DataInvalidException("Name cannot be null");
            var success = await _repo.AddHighlight(dataForAddingHighlight);
            if (success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Failed to Add Highlight");
        }

        [SwaggerOperation(Description = " This route is for deleting an event highlight. Only admins can access this route. ")]
        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        public async Task<ActionResult<OkResponse>> Delete(DataForDeletingHighlightDto dataForDeletingHighlight)
        {
            var success = await _repo.DeleteHighlight(dataForDeletingHighlight);
            if (success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Error deleting the Highlight");
        }
    }
}