using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return response.Count() > 0 ? Ok(response) : NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> NewRole([FromBody] CreateRoleRequest request)
        {
            await _service.CreateRole(request);
            return Ok();
        }
    }
}
