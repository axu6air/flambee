using Flambee.Core.Domain.User;
using Flambee.WebAPI.DataTransferModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.User
{
    public class UserFactory : IUserFactory
    {
        public UserProfileModel PrepareUserProfileModel(UserInfo user)
        {
            if (user == null && user.ApplicationUser == null)
                return null;

            var model = new UserProfileModel()
            {
                UserId = user.ApplicationUserId,
                PhoneNumber = user.PhoneNumber,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email = user.ApplicationUser.Email,
            };

            return model;
        }
    }
}
