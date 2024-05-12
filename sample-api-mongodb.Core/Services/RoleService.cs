using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class RoleService(IRoleRepository _repository, RoleManager<ApplicationRole> _roleManager) : IRoleService
    {
        public async Task<List<Roles>> GetAll()
        {
            var query = await _repository.GetAll();
            return query;
        }

        public async Task CreateRole(CreateRoleRequest request)
        {
            var role = new ApplicationRole { Name = request.RoleName, Active = request.Active };
            await _roleManager.CreateAsync(role);
        }
    }
}
