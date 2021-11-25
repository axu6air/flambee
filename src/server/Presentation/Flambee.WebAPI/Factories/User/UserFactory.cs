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

        public void PrepareUserUpdate(UserProfileSubmitModel userProfile, User user)
        {
            if (userProfile == null || user == null)
                throw new NullReferenceException();

            user.UserInfo.FirstName = userProfile.FirstName;
            user.UserInfo.LastName = userProfile.LastName;

            if (user.PhoneNumber != userProfile.PhoneNumber)
            {
                user.PhoneNumber = userProfile.PhoneNumber;
                user.PhoneNumberConfirmed = false;
            }

            if (user.Email != userProfile.Email)
            {
                user.Email = userProfile.Email;
                user.EmailConfirmed = false;
            }
        }
    }
}
