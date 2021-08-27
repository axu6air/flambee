using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Auth
{
    public class UsernameAvailabilityResponseModel : BaseResponseModel
    {
        public UsernameAvailabilityResponseModel(bool available)
        {
            base.HandleLocally = true;
            this.Available = available;
        }

        public bool Available { get; set;  }

    }
}
