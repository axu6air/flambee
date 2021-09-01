using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Flambee.Service.AppServiceProviders.User;
using Flambee.WebAPI.DataTransferModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.User
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserService _userService;

        public UserFactory(IUserService userService)
        {
            _userService = userService;
        }

        public UserProfileResponseModel PrepareUserProfileResponseModel(UserInfo user)
        {
            if (user == null && user.ApplicationUser == null)
                return null;

            var model = new UserProfileResponseModel()
            {
                UserId = user.ApplicationUserId,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.ApplicationUser.Email,
            };

            return model;
        }

        public async Task UpdateUserProfile(UserProfileSubmitModel userProfile, ApplicationUser user)
        {
            var userInfo = await _userService.GetUserInfoById(userProfile.UserId);

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

            await _userService.UpdateUserInfo(userInfo);


        }
    }
}
