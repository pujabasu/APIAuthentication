using Microsoft.AspNetCore.Mvc;

namespace ApiAuthenticationWithApiKey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleApiController : ControllerBase
    {
        public SampleApiController() { }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
