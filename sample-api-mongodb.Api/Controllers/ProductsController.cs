using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Api.Responses;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.Entities;
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
            var response = new Response<List<Products>>();
            try
            {
                var query = await _service.GetAll();
                if (query.Count() > 0)
                {
                    response.value = query;
                    response.success = true;
                    response.message = Constants.StatusMessage.Fetching_Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Ok(response);
        }

        [Authorize(Roles = "Developer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new Response<Products>();
            try
            {
                var query = await _service.Get(id);
                if (query != null)
                {
                    response.value = query;
                    response.success = true;
                    response.message = Constants.StatusMessage.Fetching_Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Ok(response);
        }
    }
}
