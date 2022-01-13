using Flambee.Core.Domain.UserDetails;
using Flambee.Service.AppServiceProviders;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceFactories
{
    public class UserBaseService 
    {
        private readonly IUserService _userService;
        private readonly ObjectId _userId;

        public User user { get; set; }
        public UserBaseService(
                IUserService userService,
                ObjectId userId
            )
        {
            _userService = userService;
            _userId = userId;

            InitilizeUser();
        }

        private async void InitilizeUser()
        {
            user = await _userService.FindById(_userId);
        }
    }
}
