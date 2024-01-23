using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Response<List<Products>>> GetAll();
        public Task<Response<Products>> Get(int id);
        public Task<Response<Products>> Insert(ProductDTO model);
        public Task<Response<Products>> Update(ProductDTO model);
        public Task<Response<Products>> Delete(int id);
    }
}
