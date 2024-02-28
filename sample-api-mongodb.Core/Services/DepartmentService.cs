using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDTO> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(DepartmentDTO model)
        {
            throw new NotImplementedException();
        }

        public Task Update(DepartmentDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
