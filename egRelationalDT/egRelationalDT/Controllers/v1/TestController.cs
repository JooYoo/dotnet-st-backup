using Microsoft.AspNetCore.Mvc;

namespace egRelationalDT.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]

    [Route("api/[controller]")]
    
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("Test Controller V1");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult Get12()
        {
            return Ok("Test Controller V1.2");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.9")]
        public IActionResult Get19()
        {
            return Ok("Test Controller V1.9");
        }
    }
}
