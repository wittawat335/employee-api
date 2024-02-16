using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver.Linq;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Exceptions;
using sample_api_mongodb.Core.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace sample_api_mongodb.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(
            UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
                    {
                        new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new (JwtRegisteredClaimNames.Sub, user.Email!),
                        new (JwtRegisteredClaimNames.Email, user.Email!),
                        new (JwtRegisteredClaimNames.Name, user.UserName!),
                    };

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim("roles", x));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["AppSettings:JWT:key"]!));
            var expires = DateTime.Now
                .AddMinutes(Int16.Parse(_configuration["AppSettings:JWT:Expires"]!));
            var creds =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["AppSettings:JWT:Issuer"],
                audience: _configuration["AppSettings:JWT:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            var response = new LoginResponse();
            response.token = new JwtSecurityTokenHandler().WriteToken(token);
            response.refreshToken = RefreshToken();
            response.userId = user.Id.ToString();
            response.roles = new List<string>(roles);
            response.email = user.Email!;
            response.username = user.UserName!;
            response.fullname = user.FullName;
            response.message = "Login Successfully";
            response.success = true;

            return response;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = new LoginResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return response;

            var verifyResult = _userManager
                .PasswordHasher
                .VerifyHashedPassword(user, user.PasswordHash!, request.Password);

            if (verifyResult == PasswordVerificationResult.Success)
                response = await CreateToken(user);
            else
                response.message = "Email / Password not valid";

            return response;
        }

        public string RefreshToken() => 
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public async Task<RegisterReaponse> RegisterAsync(RegisterRequest request)
        {
            var response = new RegisterReaponse();
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
                var createUserResult =
                    await _userManager.CreateAsync(user, request.Password);
                if (!createUserResult.Succeeded)
                {
                    response.Message = $"Create user failed {createUserResult?.Errors?.First()?.Description}";
                }
                else
                {
                    var addUserToRoleResult =
                        await _userManager.AddToRoleAsync(user, "User");
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

            return response;
        }
    }
}
