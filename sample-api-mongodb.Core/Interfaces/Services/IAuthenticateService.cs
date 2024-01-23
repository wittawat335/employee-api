using sample_api_mongodb.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IAuthenticateService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterReaponse> RegisterAsync(RegisterRequest request);
        Task CreateRole(CreateRoleRequest request);
    }
}
