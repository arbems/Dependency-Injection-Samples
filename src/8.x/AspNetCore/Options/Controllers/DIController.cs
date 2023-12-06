using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DbContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DIController : ControllerBase
    {
        private readonly ColorOptions _colorOptions;

        public DIController(IOptions<ColorOptions> colorOptions)
        {
            _colorOptions = colorOptions.Value;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_colorOptions.Color);
        }
    }
}
