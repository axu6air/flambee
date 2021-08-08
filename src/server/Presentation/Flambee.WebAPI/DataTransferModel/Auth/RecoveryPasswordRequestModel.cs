using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel.Auth
{
    public class RecoveryPasswordRequestModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string ClientRecoveryPasswordUrl { get; set; }
    }
}
