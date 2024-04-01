using Amazon.Runtime;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver.Linq;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Exceptions;
using sample_api_mongodb.Core.Interfaces.Services;
using System.Data;
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

        public AuthenticateService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null)
                {
                    response.token = GenerateToken(user, roles);
                    response.refreshToken = GenerateRefreshToken();
                    response.userId = user.Id.ToString();
                    response.roles = new List<string>(roles);
                    response.email = user.Email!;
                    response.username = user.UserName!;
                    response.fullname = user.FullName;
                    response.message = Constants.StatusMessage.LoginSuccess;
                    response.success = true;

                    user.RefreshToken = response.refreshToken;
                    var RefreshTokenValidityInDays
                       = Convert.ToInt64(_configuration[Constants.JWT.RefreshTokenValidityInDays]);
                    user.RefreshTokenExpiryTime 
                        = DateTime.Now.AddDays(RefreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);
                }
            }
            else response.message = "Email / Password not valid";

            return response;
        }

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
                    //FullName = request.Fullname,
                    Email = request.Email,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = request.Username,
                    EmailConfirmed = true,
                };
                var createUserResult =
                    await _userManager.CreateAsync(user, request.Password);
                if (!createUserResult.Succeeded)
                {
                    response.Message = 
                        $"Create user failed {createUserResult?.Errors?.First()?.Description}";
                }
                else
                {
                    var addUserToRoleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (!addUserToRoleResult.Succeeded)
                    {
                        response.Message = 
                            $"Create user succeeded but could not add user to role " +
                            $"{addUserToRoleResult?.Errors?.First()?.Description}";
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

        public async Task<LoginResponse> GetRefreshToken(RefreshTokenDTO model)
        {
            LoginResponse response = new();
            var principal = GetPrincipalFromExpiredToken(model.Token);
            string username = principal.Identity!.Name!;
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user!);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                response.message = Constants.StatusMessage.RefreshTokenInvalid;
                return response;
            }

            var newToken = GenerateToken(user, roles);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            response.token = newToken;
            response.refreshToken = newRefreshToken;
            response.userId = user.Id.ToString();
            response.roles = new List<string>(roles);
            response.email = user.Email!;
            response.username = user.UserName!;
            response.fullname = user.FullName;
            response.message = Constants.StatusMessage.RefreshTokenSuccess;
            response.success = true;

            return response;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _configuration[Constants.JWT.Issuer],
                ValidAudience = _configuration[Constants.JWT.Audience],
                IssuerSigningKey
                   = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration[Constants.JWT.Key]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private static string GenerateRefreshToken()
           => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private string GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
                    {
                        new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new (JwtRegisteredClaimNames.Sub, user.Email!),
                        new (JwtRegisteredClaimNames.Email, user.Email!),
                        new (JwtRegisteredClaimNames.Name, user.UserName!),
                        new Claim(ClaimTypes.Name, user.UserName!),
                    };

            var roleClaims = roles.Select(x => new Claim("roles", x));
            claims.AddRange(roleClaims);

            var key
                = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration[Constants.JWT.Key]!));

            var expires = DateTime.Now
                .AddMinutes(Int16.Parse(_configuration[Constants.JWT.TokenExpiresInMinute]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration[Constants.JWT.Issuer],
                audience: _configuration[Constants.JWT.Audience],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new NotFoundException("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
