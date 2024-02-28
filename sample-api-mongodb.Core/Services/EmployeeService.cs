using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDTO>> GetAll()
        {
            var query = await _repository.GetAll();
            return _mapper.Map<List<EmployeeDTO>>(query);
        }

        public async Task<EmployeeDTO> GetById(string id)
        {
            var query = await _repository.FindByIdAsync(id);
            return _mapper.Map<EmployeeDTO>(query);
        }

        public async Task Insert(EmployeeDTO model)
        {
            model.EmployeeId = GenerateId();
            await _repository.InsertOneAsync(_mapper.Map<Employee>(model));
        }

        public async Task Update(EmployeeDTO model)
        {
            var query = await _repository.FindOneAsync(_ => _.EmployeeId == model.EmployeeId);
            if (query != null) 
            {
                await _repository.ReplaceOneAsync(_mapper.Map(model, query));
            }
        }

        public async Task Delete(string id) => await _repository.DeleteByIdAsync(id);
        private string GenerateId()
        {
            string empId;
            var query = _repository.AsQueryable();
            var lastEmp = query.OrderByDescending(_ => _.EmployeeId).FirstOrDefault();
            if (lastEmp == null)
            {
                empId = "EMP001";
            }
            else
            {
                var result = Int32.Parse(lastEmp.EmployeeId!.Substring(3)) + 1;
                if (result < 10) {
                    empId = "EMP00" + result;
                }
                else if(result >= 10 && result < 100)
                {
                    empId = "EMP0" + result;
                }
                else
                {
                    empId = "EMP" + result;
                }
            }

            return empId;
        }
    }
}
