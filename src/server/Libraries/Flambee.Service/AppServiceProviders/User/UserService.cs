using Flambee.Core;
using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IRepository<UserInfo> userInfoRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userInfoRepository = userInfoRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserInfo> GetUserInfoById(int id)
        {
            if (id < 1)
                return null;

            var userInfo = await _userInfoRepository.GetByIdAsync(id);

            if (userInfo != null)
            {
                userInfo.ApplicationUser = await _userManager.FindByIdAsync(userInfo.ApplicationUserId.ToString());
            }

            return userInfo;
        }

        public async Task<UserInfo> GetUserInfoById(Guid id)
        {
            return await _userInfoRepository.GetByProperty(x => x.ApplicationUserId == id);
        }

        public async Task<IList<UserInfo>> GetUserInfoByIds(IList<int> ids)
        {
            return await _userInfoRepository.GetByIdsAsync((IList<object>)ids);
        }

        public async Task<IList<UserInfo>> GetUserInfoByIds(IList<Guid> ids)
        {
            var applicationUsers = await _userManager.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
            var userInfo = await _userInfoRepository.GetByIdsAsync((IList<object>)applicationUsers.Select(x => x.Id).ToList());

            return userInfo;
        }

        public async Task<UserInfo> CreateUserInfo(UserInfo userInfo)
        {
            return await _userInfoRepository.InsertAsync(userInfo);
        }
        public async Task<UserInfo> UpdateUserInfo(UserInfo userInfo)
        {
            return await _userInfoRepository.UpdateAsync(userInfo);
        }

        public async Task DeleteUserInfo(UserInfo userInfo)
        {
            var applicationUser = await _userManager.Users.Where(x => x.Id == userInfo.ApplicationUserId).FirstOrDefaultAsync();

            if (applicationUser != null)
            {
                await _userInfoRepository.DeleteAsync(userInfo);
                await _userManager.DeleteAsync(applicationUser);
            }

        }

        public async Task<ApplicationUser> GetLoggedInApplicationUserAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user != null)
            {
                user.PasswordHash = null;
                user.UserInfo = _userInfoRepository.GetAllAsync(x => x.ApplicationUserId == user.Id).Result.FirstOrDefault();
            }

            return user;
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetApplicationUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }


    }
}
