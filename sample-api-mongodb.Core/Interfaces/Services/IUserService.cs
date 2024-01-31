using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<List<UserDTO>>> GetAll();
        Task<Response<UserDTO>> Insert(UserDTO model);
        Task<Response<UserDTO>> Update(UserDTO model);
        Task<Response<string>> Delete(string id);
    }
}
