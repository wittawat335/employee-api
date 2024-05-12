using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Exceptions;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class DepartmentService(IGenericRepository<Department> _repository, IMapper _mapper) : IDepartmentService
    {
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
            var checkDup = checkDupilcate(model);
            if (checkDup) 
                throw new BadRequestException("Dupilcate DepartmentName");

            model.DepartmentId = GenerateId();
            model.CreatedOn = DateTime.UtcNow;
            model.ModifiedOn = DateTime.UtcNow;
            await _repository.InsertOneAsync(_mapper.Map<Department>(model));
        }


        public async Task Update(DepartmentDTO model)
        {
            var query = await 
                _repository.FindOneAsync(_ => _.DepartmentId == model.DepartmentId);
            if (query != null)
            {
                model.CreatedOn = query.CreatedOn;
                model.ModifiedOn = DateTime.UtcNow;
                await _repository.ReplaceOneAsync(_mapper.Map(model, query));
            }
        }

        private bool checkDupilcate(DepartmentDTO model)
        {
            var query = _repository.FindOne(_ => _.DepartmentName == model.DepartmentName);
            if (query != null) return true;

            return false;
        }
        private string GenerateId()
        {
            string Id;
            var query = _repository.AsQueryable();
            var lastId = query.OrderByDescending(_ => _.DepartmentId).FirstOrDefault();
            if (lastId == null)
            {
                Id = "DEP001";
            }
            else
            {
                var result = Int32.Parse(lastId.DepartmentId!.Substring(3)) + 1;
                if (result < 10)
                {
                    Id = "DEP00" + result;
                }
                else if (result >= 10 && result < 100)
                {
                    Id = "DEP0" + result;
                }
                else
                {
                    Id = "DEP" + result;
                }
            }

            return Id;
        }
    }
}
