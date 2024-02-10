using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            List<UserDTO> result = new();
            var listUsermanager = _userManager.Users.ToList();
            if (listUsermanager.Count > 0)
            {
                var listUser = new List<Users>();
                foreach (var item in listUsermanager)
                {
                    var user = new Users();
                    _mapper.Map(item, user);
                    user.Roles = await _userManager.GetRolesAsync(item);
                    listUser.Add(user);
                }
                if (listUser.Count() > 0)
                {
                    result = _mapper.Map<List<UserDTO>>(listUser);
                }
            }
            return result;
        }

        public async Task Insert(RegisterRequest model)
        {
            var user = new ApplicationUser
            {
                FullName = model.Fullname,
                Email = model.Email,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                EmailConfirmed = true,
                Active = model.Active == "1" ? true : false
            };
            var createUserResult =
                await _userManager.CreateAsync(user, model.Password);
            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.Roles!);
            }
        }

        public async Task Update(UserDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.id);
            if (user != null)
            {
                user.UserName = model.username; user.Email = model.email;
                user.FullName = model.fullname; user.PhoneNumber = model.phonenumber;
                user.Active = model.active == "1" ? true : false;
           
                var result = await _userManager.UpdateAsync(user!);
                if (result.Succeeded)
                {
                    var getRoles = await _userManager.GetRolesAsync(user);
                    var removeroles =
                        await _userManager.RemoveFromRolesAsync
                        (user, getRoles.ToArray());
                    if (removeroles.Succeeded)
                    {
                        await _userManager.AddToRolesAsync(user, model.roles!);
                    }
                }
            }
        }

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var logins = user!.Logins;
                var rolesForUser = await _userManager.GetRolesAsync(user);
                foreach (var login in logins.ToList())
                {
                    await
                        _userManager
                        .RemoveLoginAsync(
                            user, login.LoginProvider, login.ProviderKey);
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<UserDTO> Get(string id)
        {
            UserDTO result = new();
            var query = await _userManager.FindByIdAsync(id);
            if (query != null)
            {
                Users users = new();
                var mapping = _mapper.Map(query, users);
                if (mapping != null)
                    result = _mapper.Map<UserDTO>(mapping);
            }

            return result;
        }
    }
}
