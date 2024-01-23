using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        //private readonly IGenericRepository<Users> _repository;
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Users>> GetAll()
        {
            try
            {
                var query = await _repository.GetAll(); 
                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
