using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.Service.AppServiceProviders;
using Flambee.WebAPI.DataTransferModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserService _userService;

        public UserFactory(IUserService userService)
        {
            _userService = userService;
        }

        public UserProfileResponseModel PrepareUserProfileResponseModel(User user)
        {
            if (user == null)
                return null;

            var model = new UserProfileResponseModel()
            {
                UserId = user.Id,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.UserInfo.FirstName,
                LastName = user.UserInfo.LastName,
                Email = user.Email,
            };

            return model;
        }

        public async Task UpdateUserProfile(UserProfileSubmitModel userProfile, User user)
        {
            var userInfo = await _userService.GetUser(userProfile.UserId);

            if (userInfo == null)
                throw new Exception(nameof(userInfo));

            foreach (PropertyInfo profileProp in userProfile.GetType().GetProperties())
            {
                foreach (PropertyInfo infoProp in userInfo.GetType().GetProperties())
                {
                    if (infoProp.Name.ToLower() == profileProp.Name.ToLower())
                    {
                        infoProp.SetValue(userInfo, profileProp.GetValue(userProfile));
                        break;
                    }
                }
            }

            user.Email = userProfile.Email;

            await _userService.UpdateUser(user);

        }
    }
}
