using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Users;
using PhoneStoreWeb.Data.Enums;
using PhoneStoreWeb.Data.Models;
using PhoneStoreWeb.Data.UnitOfWork;
using PhoneStoreWeb.Service.FileService;
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
        private readonly IFileService fileService;
        public UserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IFileService fileService) : base(mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.fileService = fileService;
        }

        public async Task<MessageResponse> ChangeStatusAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);            
            if (user.Status == AccountStatus.Active)
            {
                user.Status = AccountStatus.Locked;
            }
            else
            {
                user.Status = AccountStatus.Active;
            }
            var result = await userManager.UpdateAsync(user);
            if (result != IdentityResult.Success)
            {
                string error = GetErrors(result).FirstOrDefault();
                return new MessageResponse("error", "Cập nhật thất bại", $"Lỗi: {error}");
            }
            return new MessageResponse("success", "Cập nhật thành công");
        }

        public async Task<MessageResponse> CreateUserAsync(RegisterRequest request)
        {
            AppUser u = await userManager.FindByNameAsync(request.UserName);
            if (u != null)
            {
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: Tên người dùng đã có.");
            }
            var user = mapper.Map<RegisterRequest, AppUser>(request);
            user.ImagePath = await fileService.UploadFileAsync(request.ThumbnailImage);
            var result = await userManager.CreateAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                string error = GetErrors(result).FirstOrDefault();
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: {error}");
            }
            var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
            result = await userManager.AddToRoleAsync(user, role.Name);
            if (result != IdentityResult.Success)
            {
                string error = GetErrors(result).FirstOrDefault();
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: {error}");
            }
            return new MessageResponse("success", "Tạo mới thành công");
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
                string roleName = roles.FirstOrDefault();
                AppRole role = await roleManager.FindByNameAsync(roleName);
                usersResult[i].Role = role.Description;
            }
            return usersResult;
        }

        public async Task<string> GetImageAsync(string name)
        {
            try
            {
                using(UnitOfWork uow = new UnitOfWork())
                {
                    AppUser user = await userManager.FindByNameAsync(name); 
                    if (user is null)
                    {
                        return "Không tìm thấy!";
                    }
                    return user.ImagePath;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<UserUpdateRequest> GetUpdateRequestAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            var result = mapper.Map<UserResponse, UserUpdateRequest>(user);
            return result;
        }

        public async Task<UserResponse> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var userResult = mapper.Map<AppUser, UserResponse>(user);
            var roles = await userManager.GetRolesAsync(user);
            string roleName = roles.FirstOrDefault();
            AppRole role = await roleManager.FindByNameAsync(roleName);
            userResult.Role = role.Description;
            return userResult;
        }

        public async Task<UserResponse> GetUserByNameAsync(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            var userResult = mapper.Map<AppUser, UserResponse>(user);
            var roles = await userManager.GetRolesAsync(user);
            string roleName = roles.FirstOrDefault();
            AppRole role = await roleManager.FindByNameAsync(roleName);
            userResult.Role = role.Description;
            return userResult;
        }

        public async Task<MessageResponse> UpdateUserAsync(UserUpdateRequest request)
        {
            AppUser u = await userManager.FindByNameAsync(request.UserName);
            if (u != null && u.Id.ToString() != request.Id)
            {
                return new MessageResponse("error", "Tạo mới thất bại", $"Lỗi: Tên người dùng đã có.");
            }
            var user = await userManager.FindByIdAsync(request.Id);
            var roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles);
            await userManager.AddToRoleAsync(user, request.Role);
            user.FullName = request.FullName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.Address = request.Address;
            user.BirthDate = request.BirthDate;
            user.PhoneNumber = request.PhoneNumber;
            if (request.ThumbnailImage != null)
            {
                user.ImagePath = await fileService.UploadFileAsync(request.ThumbnailImage);
            }           
            var result = await userManager.UpdateAsync(user);
            if (result != IdentityResult.Success)
            {
                string error = GetErrors(result).FirstOrDefault();
                return new MessageResponse("error", "Cập nhật thất bại", $"Lỗi: {error}");
            }
            return new MessageResponse("success", "Cập nhật thành công");
        }
    }
}
