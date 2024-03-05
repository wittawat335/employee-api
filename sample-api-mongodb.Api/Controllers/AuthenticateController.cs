using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController(IAuthenticateService _service) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _service.LoginAsync(request);
            return response.success ? Ok(response) : BadRequest(response.message);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            var response = await _service.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response.Message);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDTO model)
        {
            if (model is null) return BadRequest("Invalid client request");
            var response = await _service.GetRefreshToken(model);
            return response.success ? Ok(response) : BadRequest(response.message);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            await _service.Revoke(username);
            return Ok("Success");
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _service.RevokeAll();
            return Ok("Success");
        }
    }
}
