﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Exceptions;
using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class UserService(UserManager<ApplicationUser> _userManager, IMapper _mapper) : IUserService
    {
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
                    user.Password = item.PasswordHash;
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

        public async Task Insert(UserDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FullName = model.fullname,
                    Email = model.email,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = model.username,
                    EmailConfirmed = true,
                    Active = model.active == "1" ? true : false
                };
                var created =
                    await _userManager.CreateAsync(user, model.password);
                if (created.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, model.roles!);
                }
                else
                {
                    throw new BadRequestException
                        ($"Create user failed {created?.Errors?.First()?.Description}");
                }
            }
            else
            {
                throw new BadRequestException($"Email : {model.email}is Duplicate ");
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

                var updated = await _userManager.UpdateAsync(user!);
                if (updated.Succeeded)
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
                else
                {
                    throw new BadRequestException
                        ($"Update user failed {updated?.Errors?.First()?.Description}");
                }
            }
            else
            {
                throw new BadRequestException($"Not found user id : {model.id}");
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
                    await _userManager
                          .RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
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
            else
            {
                throw new BadRequestException($"Not found user id : {id}");
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
                if (mapping != null) result = _mapper.Map<UserDTO>(mapping);
            }

            return result;
        }
    }
}
