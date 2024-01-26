using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task CreateRole(CreateRoleRequest request)
        {
            try
            {

                var role = new ApplicationRole
                {
                    Name = request.RoleName,
                    Active = request.Active
                };
                var createRole = await _roleManager.CreateAsync(role);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    response.Message = "Invalid email";
                    return response;
                }
                var verifyResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (verifyResult == PasswordVerificationResult.Failed)
                {
                    response.Message = "Invalid password";
                    return response;
                }
                else
                {
                    var jwtKey = _configuration.GetSection("AppSettings:JWT:key").Value;
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };
                    var roles = await _userManager.GetRolesAsync(user);
                    var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
                    claims.AddRange(roleClaims);

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.Now.AddMinutes(30);

                    var token = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        claims: claims,
                        expires: expires,
                        signingCredentials: creds

                        );
                    response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.UserId = user.Id.ToString();
                    response.Roles = roles;
                    response.Email = user.Email;
                    response.Success = true;
                    response.Message = "Login Successfully";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<RegisterReaponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterReaponse();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    response.Message = "User already exist";
                }
                else
                {
                    user = new ApplicationUser
                    {
                        FullName = request.Fullname,
                        Email = request.Email,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        UserName = request.Username,
                    };
                    var createUserResult = await _userManager.CreateAsync(user, request.Password);
                    if (!createUserResult.Succeeded)
                    {
                        response.Message = $"Create user failed {createUserResult?.Errors?.First()?.Description}";
                    }
                    else
                    {
                        var addUserToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
                        if (!addUserToRoleResult.Succeeded)
                        {
                            response.Message = $"Create user succeeded but could not add user to role {addUserToRoleResult?.Errors?.First()?.Description}";
                        }
                        else
                        {
                            response.Message = Constants.StatusMessage.RegisterSuccess;
                            response.Success = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
