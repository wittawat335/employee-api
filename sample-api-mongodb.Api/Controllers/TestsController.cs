using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.Interfaces.Services;
using System.Collections.Frozen;

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

        [HttpGet("FrozenSet")]
        public IActionResult FrozenSet()
        {
            var list = _service.GetCities();
            var frozenSet = list.ToFrozenSet();
            var city = frozenSet.Where(_ => _ == "London" || _ == "Bangkok");

            return Ok(city);
        }
    } 
}
