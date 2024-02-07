using sample_api_mongodb.Core.DTOs;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> Get(int id);
        Task Insert(ProductDTO model);
        Task Update(ProductDTO model);
        Task Delete(string id);
    }
}
