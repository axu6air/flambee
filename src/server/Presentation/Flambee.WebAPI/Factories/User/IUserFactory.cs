using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Flambee.WebAPI.DataTransferModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories
{
    public interface IUserFactory
    {
        UserProfileResponseModel PrepareUserProfileResponseModel(User user);

        Task UpdateUserProfile(UserProfileSubmitModel model, User user);
    }
}
