using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;

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

        public async Task Delete(string id) => await _repository.DeleteByIdAsync(id);

        public async Task<DepartmentDTO> GetById(string id)
        {
            var query = await _repository.FindByIdAsync(id);
            return _mapper.Map<DepartmentDTO>(query);
        }

        public async Task<List<DepartmentDTO>> GetAll()
        {
            var query = await _repository.GetAll();
            return _mapper.Map<List<DepartmentDTO>>(query);
        }

        public async Task Insert(DepartmentDTO model)
        {
            await _repository.InsertOneAsync(_mapper.Map<Department>(model));
        }


        public async Task Update(DepartmentDTO model)
        {
            var query = await _repository.FindOneAsync(_ => _.DepartmentId == model.DepartmentId);
            if (query != null)
            {
                await _repository.ReplaceOneAsync(_mapper.Map(model, query));
            }
        }
    }
}
