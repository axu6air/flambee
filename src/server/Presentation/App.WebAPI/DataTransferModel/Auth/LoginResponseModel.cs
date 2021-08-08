using App.WebAPI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.DataTransferModel.Auth
{
    public class LoginResponseModel : BaseErrorModel
    {
        public string Token { get; set; }
        public UserInfoModel UserInfoModel { get; set; }
    }
}
