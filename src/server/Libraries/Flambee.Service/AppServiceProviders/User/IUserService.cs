using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.User
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoById(int id);
        Task<UserInfo> GetUserInfoById(Guid id);
        Task<IList<UserInfo>> GetUserInfoByIds(IList<int> ids);
        Task<IList<UserInfo>> GetUserInfoByIds(IList<Guid> ids);
        Task<UserInfo> CreateUserInfo(UserInfo userDetails);
        Task<UserInfo> UpdateUserInfo(UserInfo userDetails);
        Task DeleteUserInfo(UserInfo userDetails);
        Task<ApplicationUser> GetLoggedInApplicationUserAsync();
        
        Task<ApplicationUser> GetApplicationUserAsync(string email);
        Task<ApplicationUser> GetApplicationUser(Guid id);
    }
}
