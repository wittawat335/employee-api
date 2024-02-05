using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<List<Roles>> GetAll();
        Task CreateRole(CreateRoleRequest request);
    }
}
