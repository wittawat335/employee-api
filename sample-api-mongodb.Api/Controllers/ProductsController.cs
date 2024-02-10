using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Api.Responses;

namespace sample_api_mongodb.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return response.Count() > 0 ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductDTO model)
        {
            await _service.Insert(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            await _service.Update(model);
            return Ok();
        }


        [Authorize(Roles = "Developer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
