using Microsoft.Extensions.Options;
using MongoDB.Driver;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMongoCollection<Roles> _collection;

        public RoleRepository(IOptions<DbSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            _collection = mongoClient.GetDatabase(options.Value.DatabaseName).GetCollection<Roles>("roles");
        }

        public IQueryable<Roles> AsQueryable() => _collection.AsQueryable();

        public async Task<List<Roles>> GetAll() => await _collection.Find(x => true).ToListAsync();
    }
}
