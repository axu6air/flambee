using Flambee.Core.Domain.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.Authentication
{
    public interface IAuthService
    {
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null);
        Task SignOutAsync();
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser applicationUser, string token, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser applicationUser);
    }
}
