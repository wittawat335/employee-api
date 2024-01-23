using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Products>  _repository;

        public ProductService(IGenericRepository<Products> repository)
        {
            _repository = repository;
        }

        public async Task<Products> Get(int id)
        {
            try
            {
                return await _repository.FindOneAsync(x => x.ProductId == id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Products>> GetAll()
        {
            try
            {
                return await _repository.GetAll(); 
            }
            catch
            {
                throw;
            }
        }
    }
}
