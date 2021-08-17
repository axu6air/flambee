using Flambee.WebAPI.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Authentication
{
    public class RegistrationResponseModel : BaseErrorModel
    {
        public bool Succeeded { get; set; }
        public Guid UserId { get; set; }
    }
}
