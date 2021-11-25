using Flambee.WebAPI.DataTransferModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.Authentication
{
    public class RegistrationResponseModel : BaseErrorModel
    {
        public bool Succeeded { get; set; }
        public object UserId { get; set; }
    }
}
