using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<UserDTO>>> GetAll()
        {
            var response = new Response<List<UserDTO>>();
            try
            {
                var query = await _repository.GetAll();
                if (query.Count() > 0)
                {
                    response.value = _mapper.Map<List<UserDTO>>(query);
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
