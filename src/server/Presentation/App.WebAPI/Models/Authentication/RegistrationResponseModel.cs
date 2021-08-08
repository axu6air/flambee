using App.WebAPI.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.Models.Authentication
{
    public class RegistrationResponseModel : BaseErrorModel
    {
        public bool Succeeded { get; set; }
    }
}
