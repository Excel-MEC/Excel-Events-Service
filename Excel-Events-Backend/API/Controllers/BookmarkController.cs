using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Bookmark;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for bookmarking various events")]
    [Route("/bookmark")]    
    [ApiController]

    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _repo;
        public BookmarkController(IBookmarkRepository repo)
        {
            _repo = repo;
        }
        [SwaggerOperation(Description = "Event Registration")]
        [HttpPost]
        public async Task<ActionResult> Add(DataFromClientDto data)
        {
            int excelId = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
            var success = await _repo.Add(excelId, data.Id);
            if(success) return Ok(new OkResponse { Response = "Success" });
            throw new Exception("Problem in bookmarking the event");
        }

        [SwaggerOperation(Description = " Remove all bookmarks of an user")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult> RemoveAll(DataFromClientDto data)
        {
            var success = await _repo.RemoveAll(data.Id);
            if(success) return Ok(new OkResponse { Response = "Success"});
            throw new Exception("Problem clearing bookmarks. Check out the userid");
        }

        [SwaggerOperation(Description = "List of events registered by a user")]
        [HttpGet]
        public async Task<ActionResult<List<EventForBookmarkListViewDto>>> EventList()
        {
            int id = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            var list = await _repo.EventList(id);
            return list;
        }

        [SwaggerOperation(Description = "Removes a particular bookmarked event")]
        [HttpGet("{eventId}/users")]
        public async Task<ActionResult<List<int>>> Remove(string eventId)
        {
            int excelId = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            var success = await _repo.Remove(excelId, int.Parse(eventId));
            if(success) return Ok(new OkResponse { Response = "Success"});
            throw new Exception("Problem removing the bookmarked event");
        }

        

    }
}