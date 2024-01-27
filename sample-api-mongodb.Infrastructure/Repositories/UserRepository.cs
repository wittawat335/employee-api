using Microsoft.Extensions.Options;
using MongoDB.Driver;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;

namespace sample_api_mongodb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Users> _collection;

        public UserRepository(IOptions<DbSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _collection = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Users>("users");
        }

        public IQueryable<Users> AsQueryable() => _collection.AsQueryable();

        public async Task<List<Users>> GetAll() => await _collection.Find(x => true).ToListAsync();
    }
}
