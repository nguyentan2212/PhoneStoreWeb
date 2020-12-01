using PhoneStoreWeb.Communication.Authentication;
using PhoneStoreWeb.Communication.ResponseResult;
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
        public Task<string> CreateUserAsync(RegisterRequest request);
        public List<RoleResponse> GetAllRoles();
        public Task<string> ChangeStatusAsync(string id);
    }
}
