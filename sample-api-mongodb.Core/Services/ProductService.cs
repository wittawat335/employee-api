using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using System.Collections.Generic;

namespace sample_api_mongodb.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Products> _repository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var query = await _repository.FindOneAsync(x => x.ProductId == id);
            return _mapper.Map<ProductDTO>(query);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var products = new List<ProductDTO>();
            var query = await _repository.GetAll();
            if (query.Count() > 0)
            {
                products = _mapper.Map<List<ProductDTO>>(query);
            }
            return products;
        }

        public async Task Insert(ProductDTO model)
        {
            var id = _repository.AsQueryable().OrderByDescending(x => x.ProductId).Select(b => b.ProductId).First();
            if (id >= 0)
            {
                model.ProductId = id + 1;
                await _repository.InsertOneAsync(_mapper.Map<Products>(model));
            }
        }

        public async Task Update(ProductDTO model)
        {
            var id = await _repository.FindOneAsync(x => x.ProductId == model.ProductId);
            if(id != null)
            {
                await _repository.ReplaceOneAsync(_mapper.Map(model, id));
            }
        }

        public async Task Delete(string id) => await _repository.DeleteByIdAsync(id);
    }
}
