using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;
using System.Net;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController(IAuthenticateService _service) : ControllerBase
    {
        //[HttpPost]
        //[Route("refresh-token")]
        //public async Task<IActionResult> RefreshToken()
        //{

        //    var response = await _service.LoginAsync(request);

        //    return Ok(response);
        //}

        [HttpPost]
        [Route("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            return response.success ? Ok(response) : BadRequest(response.message);
        }
        [HttpPost]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RegisterReaponse))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            var response = await _service.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost]
        [Route("createRole")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            await _service.CreateRole(request);
            return Ok("success");
        }
    }
}
