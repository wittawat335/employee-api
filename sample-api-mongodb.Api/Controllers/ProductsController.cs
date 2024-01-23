using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }
    }
}
