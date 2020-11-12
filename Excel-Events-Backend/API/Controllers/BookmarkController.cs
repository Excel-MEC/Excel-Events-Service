using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Bookmark;
using API.Dtos.Registration;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The routes under this controller are for performing CRUD operations on Bookmarks table.")]
    [Authorize]
    [Route("/bookmark")]    
    [ApiController]

    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _repo;
        public BookmarkController(IBookmarkRepository repo)
        {
            _repo = repo;
        }

        [SwaggerOperation(Description = " This route is for listing all the events bookmarked by a user. ")]
        [HttpGet]
        public async Task<ActionResult<List<EventForBookmarkListViewDto>>> EventList()
        {
            var id = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            return Ok(await _repo.EventList(id));
        }
        
        [SwaggerOperation(Description = " This route is for bookmarking an event. ")]
        [HttpPost]
        public async Task<ActionResult<Bookmark>> Add(DataForRegistrationDto dataForRegistration)
        {
            var excelId = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
            return Ok( await _repo.Add(excelId, dataForRegistration.EventId));
        }

        [SwaggerOperation(Description = " This route is to remove a bookmarked event by the user. ")]
        [HttpDelete("{eventId}")]
        public async Task<ActionResult<Bookmark>> Remove(int eventId)
        {
            var excelId = int.Parse(this.User.Claims.First(x => x.Type == "user_id").Value);
            return Ok(await _repo.Remove(excelId, eventId));
        }
        
        [SwaggerOperation(Description = " This route is to remove all bookmarks of a user when the user account is deleted. Only admins can access this route. ")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{userId}")]
        public async Task<ActionResult<List<Bookmark>>> RemoveAll(int userId)
        {
            return Ok(await _repo.RemoveAll(userId));
        }
    }
}