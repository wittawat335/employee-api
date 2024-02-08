using sample_api_mongodb.Core.DTOs;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> Get(string id);
        Task Insert(RegisterRequest model);
        Task Update(UserDTO model);
        Task Delete(string id);
    }
}
