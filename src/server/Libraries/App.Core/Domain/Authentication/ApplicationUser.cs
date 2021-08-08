using App.Core.Domain.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Domain.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public UserInfo UserInfo { get; set; }
    }
}
