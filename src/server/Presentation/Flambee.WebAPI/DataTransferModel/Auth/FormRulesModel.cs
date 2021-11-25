using Flambee.Core.Configuration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Auth
{
    public class FormRulesModel : BaseResponseModel
    {
        public string Username => UserRules.UsernameExpression;
    }
}
