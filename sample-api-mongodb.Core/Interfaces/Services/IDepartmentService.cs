using sample_api_mongodb.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAll();
        Task<DepartmentDTO> Get(string id);
        Task Insert(DepartmentDTO model);
        Task Update(DepartmentDTO model);
        Task Delete(string id);
    }
}
