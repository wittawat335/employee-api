using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
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
        private readonly IGenericRepository<Department> _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IGenericRepository<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDTO> Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DepartmentDTO>> GetAll()
        {
            var query = await _repository.GetAll();
            return _mapper.Map<List<DepartmentDTO>>(query);
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
