using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<List<UserDTO>>> GetAll();
    }
}
