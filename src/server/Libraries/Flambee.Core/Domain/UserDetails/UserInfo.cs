using Flambee.Core.Domain.Authentication;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flambee.Core.Domain.UserDetails
{
    public class UserInfo : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}