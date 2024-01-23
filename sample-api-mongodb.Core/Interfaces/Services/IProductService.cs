using sample_api_mongodb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IProductService
    {
        public Task<List<Products>> GetAll();
        public Task<Products> Get(int id);
    }
}
