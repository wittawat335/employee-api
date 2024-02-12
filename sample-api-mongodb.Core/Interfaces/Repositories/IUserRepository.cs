using sample_api_mongodb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAll();
        IQueryable<Users> AsQueryable();
    }
}

