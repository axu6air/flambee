using App.Core.Domain.Authentication;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Domain.User
{
    public class UserInfo : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}