using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public RoleService(IRoleRepository repository, RoleManager<ApplicationRole> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }

        public async Task<Response<List<Roles>>> GetAll()
        {
            var response = new Response<List<Roles>>();
            try
            {
                var query = await _repository.GetAll();
                if (query.Count() > 0)
                {
                    response.value = query;
                    response.success = true;
                    response.message = Constants.StatusMessage.Fetching_Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task CreateRole(CreateRoleRequest request)
        {
            try
            {
                var role = new ApplicationRole { Name = request.RoleName, Active = request.Active };
                var createRole = await _roleManager.CreateAsync(role);
            }
            catch
            {
                throw;
            }
        }
    }
}
