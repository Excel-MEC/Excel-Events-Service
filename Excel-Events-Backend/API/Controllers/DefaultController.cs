using System;
using System.Linq;
using API.Dtos.Default;
using API.Extensions;
using API.Models.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The Routes under this controller do not need authorization.")]
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [Authorize(Roles = "Admins, User")]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Success");
            // return Ok(new OkResponse { Response = this.User.Claims.First(i => i.Type == "user_id").Value });
        }

        [HttpGet("constants")]
        public ActionResult GetConstants()
        {
            ConstantsForClientDto constants = new ConstantsForClientDto
            {
                Category = Constants.Category,
                EventType = Constants.EventType,
                EventStatus = Constants.EventStatus
            };
            return Ok(constants);
        }
    }
}