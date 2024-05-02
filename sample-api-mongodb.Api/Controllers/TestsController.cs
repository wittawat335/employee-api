using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        public string GenerateGreetText()
        {
            var dateTimeNow= DateTime.Now;
            return dateTimeNow.Hour switch
            { 
                >= 5 and < 12 => "Morning",
                >= 12 and < 18 => "Afternoon",
                _ => "Evering"
            };
        }
    }
}
