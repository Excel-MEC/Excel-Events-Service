using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Highlight;
using API.Models;
using API.Models.Custom;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for perfoming CRUD optrations on Highlights table.")]
    [Route("/highlights")]
    [ApiController]
    public class HighlightController : ControllerBase
    {
        private readonly IHighlightRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEventService _service;

        public HighlightController(IHighlightRepository repo, IMapper mapper, IEventService service)
        {
            _repo = repo;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Highlight>>> Get()
        {
            List<Highlight> events = await _repo.GetHighlights();
            return Ok(events);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<OkResponse>> Add([FromForm] DataForAddingHighlightDto dataForAddingHighlight)
        {
            bool success = await _repo.AddHighlight(dataForAddingHighlight);
            if (success)
                return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Failed to Add Highlight");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("delete")]
        public async Task<ActionResult<OkResponse>> Delete(DataForDeletingHighlightDto dataForDeletingHighlight)
        {
            bool success = await _repo.DeleteHighlight(dataForDeletingHighlight);
            if (success)
                return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Error deleting the Highlight");
        }
    }
}