using AutoMapper;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;

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

        public async Task<Response<ProductDTO>> Get(int id)
        {
            var response = new Response<ProductDTO>();
            try
            {
                var query = await _repository.FindOneAsync(x => x.ProductId == id);
                if (query != null)
                {
                    response.value = _mapper.Map<ProductDTO>(query);
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

        public async Task<Response<List<ProductDTO>>> GetAll()
        {
            var response = new Response<List<ProductDTO>>();
            try
            {
                var query = await _repository.GetAll();
                if (query.Count() > 0)
                {
                    response.value = _mapper.Map<List<ProductDTO>>(query);
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

        public async Task<Response<Products>> Insert(ProductDTO model)
        {
            var response = new Response<Products>();
            try
            {
                var id = _repository.AsQueryable().OrderByDescending(x => x.ProductId).Select(b => b.ProductId).First();
                model.ProductId = id + 1;
                await _repository.InsertOneAsync(_mapper.Map<Products>(model));
                response.success = true;
                response.message = Constants.StatusMessage.InsertSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Products>> Update(ProductDTO model)
        {
            var response = new Response<Products>();
            try
            {
                var id = await _repository.FindOneAsync(x => x.ProductId == model.ProductId);
                await _repository.ReplaceOneAsync(_mapper.Map(model, id));
                response.success = true;
                response.message = Constants.StatusMessage.UpdateSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Products>> Delete(int id)
        {
            var response = new Response<Products>();
            try
            {
                await _repository.DeleteOneAsync(x => x.ProductId == id);
                response.success = true;
                response.message = Constants.StatusMessage.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }
    }
}
