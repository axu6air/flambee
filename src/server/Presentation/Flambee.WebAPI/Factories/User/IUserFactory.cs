using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Flambee.WebAPI.DataTransferModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.User
{
    public interface IUserFactory
    {
        UserProfileResponseModel PrepareUserProfileResponseModel(UserInfo user);

        Task UpdateUserProfile(UserProfileSubmitModel model, ApplicationUser user);
    }
}
