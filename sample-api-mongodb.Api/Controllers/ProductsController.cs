using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductDTO model)
        {
            var response = await _service.Insert(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            var response = await _service.Update(model);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            return Ok(response);
        }
    }
}
