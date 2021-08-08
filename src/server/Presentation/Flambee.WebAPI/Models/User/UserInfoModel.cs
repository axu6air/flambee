using Flambee.Core.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models.User
{
    public class UserInfoModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid ApplicationUserId { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}
