using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAll();
        Task<Response<ProductDTO>> Get(int id);
        Task<Response<Products>> Insert(ProductDTO model);
        Task<Response<Products>> Update(ProductDTO model);
        Task<Response<Products>> Delete(int id);
    }
}
