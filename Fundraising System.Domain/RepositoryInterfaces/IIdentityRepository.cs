using Fundraising_System.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Domain.RepositoryInterfaces
{
    public interface IIdentityRepository
    {
        Task<ApplicationUser> FindByEmailAsync(string Email);
        Task<ApplicationUser> FindByNameAsync(string Name);
        Task<ApplicationUser> FindByIdAsync(string id);
        Task<IdentityResult>CreateAsync(ApplicationUser applicationUser,string Password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string RoleName);
        Task<bool> IsInRoleAsync(ApplicationUser user, string RoleName);
        Task<IEnumerable<string>> GetRolesAsync(ApplicationUser applicationUser);
        Task<bool> RoleExistsAsync(string RoleName);
        Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser applicationUser);
        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string Password);
        Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync(string userId);  
        Task<RefreshToken> GetRefreshTokenByNameAsync(string TokenName);  
        Task<int> RevokeRefreshTokenAsync(RefreshToken Token);  //refreshToken.RevokedOn = DateTime.UtcNow;
        Task AddRefreshTokenAsync(RefreshToken? refreshToken);
    }
}
