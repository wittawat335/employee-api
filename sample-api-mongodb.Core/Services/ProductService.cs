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
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Products>  _repository;

        public ProductService(IGenericRepository<Products> repository)
        {
            _repository = repository;
        }

        public async Task<List<Products>> GetAll()
        {
            var list = await _repository.GetAll();
            return list;
        }
    }
}
