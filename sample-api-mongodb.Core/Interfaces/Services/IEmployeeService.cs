using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> GetById(string id);
        Task Insert(EmployeeDTO model);
        Task Update(EmployeeDTO model);
        Task Delete(string id);
    }
}
