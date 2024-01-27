using Amazon.Runtime.Internal;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;
using System.Collections.Generic;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserRepository _repository;
        private readonly IRoleRepository _rrepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IRoleRepository rrepository, IMapper mapper, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _rrepository = rrepository;
             _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Response<List<UserDTO>>> GetAll()
        {
            var response = new Response<List<UserDTO>>();
            try
            {
             
                var listUser = new List<Users>();
                var listUsermanager = _userManager.Users.ToList();
                foreach (var item in listUsermanager)
                {
                    var user = new Users();
                    _mapper.Map(item, user);
                    user.Roles = string.Join(", ", _userManager.GetRolesAsync(item).Result.ToArray());
                    listUser.Add(user);
                }
                if (listUser.Count() > 0)
                {
                    response.value = _mapper.Map<List<UserDTO>>(listUser);
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
