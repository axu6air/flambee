using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Authentication
{
    public class RegistrationResponseModel : BaseResponseModel
    {
        public bool Succeeded { get; set; }
        public IList<string> ErrorMessages { get; set; }
    }
}
