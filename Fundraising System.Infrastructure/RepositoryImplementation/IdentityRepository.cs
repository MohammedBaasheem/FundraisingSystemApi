
using Fundraising_System.Domain.Entities;
using Fundraising_System.Domain.RepositoryInterfaces;
using Fundraising_System.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Infrastructure.RepositoryImplementation
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly JWT _jwt;
        private readonly ApplicationDbContext _context;
        public IdentityRepository(
            UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager
         , ApplicationDbContext context)
        {
            _userManager= userManager;
            _roleManager = roleManager;
           
            _context= context;
        }
        public async Task AddRefreshTokenAsync(RefreshToken? refreshToken)
        {
           if (refreshToken != null)
           {
                _context.RefreshTokens.Add(refreshToken);   ///RefreshTokens table name==>RefreshToken
                await _context.SaveChangesAsync();
           }
         
        }
        public async  Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string RoleName)
        {
            var result = await _userManager.AddToRoleAsync(applicationUser, RoleName);
            return result;
        }
        public Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string Password)
        {
           var result = _userManager.CheckPasswordAsync(applicationUser, Password);
            return result;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string Password)
        {
            var result =await _userManager.CreateAsync(applicationUser, Password);
            return result;
        }
        public async Task<ApplicationUser> FindByEmailAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            return user;
        }
        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            return User;
        }
        public async Task<ApplicationUser> FindByNameAsync(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);
            return user;
        }
        public async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser applicationUser)
        {
            var userClims = await _userManager.GetClaimsAsync(applicationUser);
            return userClims;
        }
        public async Task<IEnumerable<RefreshToken>> GetRefreshTokensAsync(string userId)
        {
            var activeRefreshTokens = await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            return activeRefreshTokens;
        }
        public async Task<RefreshToken> GetRefreshTokenByNameAsync(string TokenName)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == TokenName);
            return refreshToken;
        }
        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser applicationUser)
        {
            var roles = await _userManager.GetRolesAsync(applicationUser);
            return roles;
        }
        public async Task<bool> IsInRoleAsync(ApplicationUser user, string RoleName)
        {
            var result =await _userManager.IsInRoleAsync(user, RoleName);
            return result;
        }
        public async Task<int> RevokeRefreshTokenAsync(RefreshToken Token)
        {
           var result= _context.RefreshTokens.Update(Token);
            await _context.SaveChangesAsync();
            return 1;
        }
        public async Task<bool> RoleExistsAsync(string RoleName)
        {
            var IsInRoles = await _roleManager.RoleExistsAsync(RoleName);
            return IsInRoles;
        }

       
    }
}
