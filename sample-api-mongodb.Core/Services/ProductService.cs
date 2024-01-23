using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Products>  _repository;

        public ProductService(IGenericRepository<Products> repository)
        {
            _repository = repository;
        }

        public async Task<Response<Products>> Get(int id)
        {
            var response = new Response<Products>();
            try
            {
                var query = await _repository.FindOneAsync(x => x.ProductId == id);
                if (query != null)
                {
                    response.value = query;
                    response.success = true;
                    response.message = Constants.StatusMessage.Fetching_Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<List<Products>>> GetAll()
        {
            var response = new Response<List<Products>>();
            try
            {
                var query = await _repository.GetAll();
                if (query.Count() > 0)
                {
                    response.value = query;
                    response.success = true;
                    response.message = Constants.StatusMessage.Fetching_Success;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
