﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.UserService
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        public UserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> CreateUserAsync(RegisterRequest request)
        {
            var user = mapper.Map<RegisterRequest, AppUser>(request);            
            var result = await userManager.CreateAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return GetErrors(result).FirstOrDefault();
            }
            var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
            result = await userManager.AddToRoleAsync(user, role.Name);
            if (result != IdentityResult.Success)
            {
                return GetErrors(result).FirstOrDefault();
            }
            return null;
        }

        public List<RoleResponse> GetAllRoles()
        {
            var roles = roleManager.Roles.ToList();
            var rolesResult = mapper.Map<List<AppRole>, List<RoleResponse>> (roles);
            return rolesResult;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList();
            var usersResult = mapper.Map<List<AppUser>, List<UserResponse>>(users); 
            for (int i = 0; i < users.Count; i++)
            {
                var roles = await userManager.GetRolesAsync(users[i]);                
                usersResult[i].Role = roles.FirstOrDefault();
            }
            return usersResult;
        }

        public async Task<UserResponse> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userResult = mapper.Map<AppUser, UserResponse>(user);
            return userResult;
        }
    }
}