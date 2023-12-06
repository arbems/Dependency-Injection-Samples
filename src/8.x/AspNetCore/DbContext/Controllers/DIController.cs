using Microsoft.AspNetCore.Mvc;

namespace DbContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DIController : ControllerBase
    {
        public DIController()
        {

        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
