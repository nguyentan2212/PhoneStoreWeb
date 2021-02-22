using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
using PhoneStoreWeb.Communication.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreWeb.Service.UserService
{
    public interface IUserService : IServiceBase
    {
        public Task<List<UserResponse>> GetAllUsersAsync();
        public Task<UserResponse> GetUserByIdAsync(string id);
        public Task<UserResponse> GetUserByNameAsync(string name);
        public Task<UserUpdateRequest> GetUpdateRequestAsync(string id);
        public Task<MessageResponse> CreateUserAsync(RegisterRequest request);
        public Task<MessageResponse> UpdateUserAsync(UserUpdateRequest request);
        public List<RoleResponse> GetAllRoles();
        public Task<MessageResponse> ChangeStatusAsync(string id);
        public Task<string> GetImageAsync(string name);
    }
}
