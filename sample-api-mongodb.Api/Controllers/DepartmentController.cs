using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService _service) : ControllerBase
    {
        //[Authorize(Roles = "Developer")]
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
        public async Task<IActionResult> Insert(DepartmentDTO model)
        {
            await _service.Insert(model); return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentDTO model)
        {
            await _service.Update(model); return Ok();
        }

        [Authorize(Roles = "Developer, Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete(id); return Ok();
        }
    }
}
