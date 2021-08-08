using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Auth
{
    public class UsernameAvailabilityResponseModel : BaseResponseModel
    {
        public UsernameAvailabilityResponseModel()
        {
            base.HandleLocally = true;
        }

        public bool Available { get; set; }

    }
}
