using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Flambee.Service.AppServiceProviders
{
    public interface IUserService
    {
        Task<User> GetUser(object id);
        Task<User> GetUser(string username = null, string email = null);
        Task<IList<User>> GetUsers(IList<object> ids);
        Task<User> FindById(object userId);
        Task<User> FindByEmail(string email);
        Task<User> FindByUsername(string userName);
        Task<User> GetLoggedInApplicationUserAsync();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<UserInfo> UpsertUserInfo(UserInfo userInfo);
        Task DeleteUser(User user);
        Task DeleteUserInfo(object userInfoId);
    }
}
