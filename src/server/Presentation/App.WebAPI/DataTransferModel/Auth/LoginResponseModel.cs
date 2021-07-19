using Flambee.WebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Auth
{
    public class LoginResponseModel : BaseResponseModel
    {
        public string Token { get; set; }
        public UserInfoModel UserInfoModel { get; set; }
    }
}
