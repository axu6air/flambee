using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.UserDetails;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.Authentication
{
    public interface IAuthService
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(UserRole role);
        Task SignInAsync(User user, bool isPersistent, string authenticationMethod = null);
        Task SignOutAsync();
        Task<IdentityResult> ResetPasswordAsync(User applicationUser, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(User applicationUser);
    }
}
