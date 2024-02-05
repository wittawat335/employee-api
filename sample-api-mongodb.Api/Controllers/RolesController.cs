using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Api.Responses;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new Response<List<Roles>>();
            response.payload = await _service.GetAll();
            return response.payload.Count() > 0 ? Ok(response.payload) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            await _service.CreateRole(request);
            return Ok();
        }
    }
}
