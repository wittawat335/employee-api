using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return response.Count() > 0 ? Ok(response) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.GetById(id); 
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EmployeeDTO model)
        {
            await _service.Insert(model); return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDTO model)
        {
            await _service.Update(model); return Ok();
        }

        [Authorize(Roles = "Developer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete(id); return Ok();
        }
    }
}
