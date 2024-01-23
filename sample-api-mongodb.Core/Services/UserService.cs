using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAll()
        {
            try
            {
                var query = await _repository.GetAll();
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
