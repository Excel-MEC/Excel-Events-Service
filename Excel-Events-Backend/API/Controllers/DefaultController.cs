using API.Models.Custom;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [SwaggerTag("The Routes under this controller do not need authorization.")]
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new OkResponse { Response = "success" });
        }
    }
}