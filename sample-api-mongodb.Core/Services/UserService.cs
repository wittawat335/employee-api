using Amazon.Runtime.Internal;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.Commons;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Core.Interfaces.Services;
using sample_api_mongodb.Core.Responses;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUserRepository repository, 
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<UserDTO>>> GetAll()
        {
            var response = new Response<List<UserDTO>>();
            var listUser = new List<Users>();
            try
            {
                var listUsermanager = _userManager.Users.ToList();
                if (listUsermanager.Count > 0)
                {
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
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<UserDTO>> Insert(UserDTO model)
        {
            var response = new Response<UserDTO>();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.email);
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Task<Response<UserDTO>> Update(UserDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> Delete(string id)
        {
            var response = new Response<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var logins = user!.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(user);
                foreach (var login in logins.ToList())
                {
                    await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                await _userManager.DeleteAsync(user);
                response.message = Constants.StatusMessage.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

    }
}
