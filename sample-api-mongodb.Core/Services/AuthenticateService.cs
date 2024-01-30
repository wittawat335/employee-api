using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<LoginResponse> CreateToken(ApplicationUser user)
        {
            var response = new LoginResponse();
            try
            {
                var jwtKey = _configuration.GetSection("AppSettings:JWT:key").Value;
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["AppSettings:JWT:Issuer"],
                    audience: _configuration["AppSettings:JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                    );

                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.UserId = user.Id.ToString();
                response.Roles = roles;
                response.Email = user.Email;
                response.Success = true;
                response.Message = "Login Successfully";

                return response;
            }
            catch
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
                if (!user.Active)
                {
                    response.Message = "user is inactive";
                    return response;
                }
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
                    response = await CreateToken(user);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Task<LoginResponse> RefreshToken(LoginResponse token)
        {
            throw new NotImplementedException();
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
                        EmailConfirmed = true,
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
