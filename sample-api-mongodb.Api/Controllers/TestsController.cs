using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController(ITestService _service) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetGenerateGreetText()
        {
            return Ok(_service.GenereateGreetText());
        }
    }
}
