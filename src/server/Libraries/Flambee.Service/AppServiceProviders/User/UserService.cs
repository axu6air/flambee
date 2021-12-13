using Flambee.Core;
using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMongoRepository<UserInfo> _userInfoRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IMongoRepository<User> userRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMongoRepository<UserInfo> userInfoRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<User> GetUser(object id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> GetUser(string username = null, string email = null)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
                return await _userRepository.Get(x => x.UserName.Equals(username) && x.Email.Equals(email));
            else if (!string.IsNullOrEmpty(username))
                return await _userRepository.Get(x => x.UserName.Equals(username));
            else if (!string.IsNullOrEmpty(email))
                return await _userRepository.Get(x => x.Email.Equals(email));

            return null;
        }

        public async Task<IList<User>> GetUsers(IList<object> ids)
        {
            return await _userRepository.GetAll(x => ids.Contains(x.Id));
        }

        public async Task<User> FindById(object userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<User> FindByUsername(string userName)
        {
            try
            {
                return await _userManager.FindByNameAsync(userName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetLoggedInApplicationUserAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
             var result = await _userManager.CreateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }

        public async Task<User> UpsertUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Id, user.Id);
            var option = new FindOneAndReplaceOptions<User> { IsUpsert = true, ReturnDocument = ReturnDocument.After };
            return await _userRepository.Collection.FindOneAndReplaceAsync(filter, user, option);
        }

        public async Task DeleteUser(User user)
        {
            user.IsDeleted = true;
            await _userRepository.Collection.FindOneAndReplaceAsync(x => x.Id == user.Id, user);
        }

        public async Task DeleteUserInfo(object userInfoId)
        {
            await _userInfoRepository.Delete(e => e.Id.Equals(userInfoId));
        }
    }
}
