 using Fundraising_System.Application.DependencyInjection;
using Fundraising_System.Application.DTOs.Requestes;
using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Application.Helpers;
using Fundraising_System.Application.UseCaseInterface;
using Fundraising_System.Domain.Entities;
using Fundraising_System.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.UseCaseImplementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly JwtSettings _jwt;
        //private readonly ApplicationDbContext _context;
        public IdentityService(IIdentityRepository identityRepository, JwtSettings jwt)
        {
            _identityRepository = identityRepository;
            _jwt = jwt;
        }
        public async Task<string> AddRloeAsync(AddRoleDto addRoleDto)
        {
            var User = await _identityRepository.FindByIdAsync(addRoleDto.UserId);
            if (User is null || !await _identityRepository.RoleExistsAsync(addRoleDto.RoleName))
            {
                return "Invali User ID or Role";
            }
            if (await _identityRepository.IsInRoleAsync(User, addRoleDto.RoleName))
            {
                return "User arealy assign in this role";
            }
            var result = await _identityRepository.AddToRoleAsync(User, addRoleDto.RoleName);
            return result.Succeeded ? "the user added to " + addRoleDto.RoleName + "Succesded" : "Something went wrong";



        }
        public async Task<AuthDto> GetTokenAsync(TokenRequestDto tokenRequestDto)
        {
            var authModel = new AuthDto();
            if (tokenRequestDto is null)
            {
                authModel.Message = "tokenRequestModel nulll !!";
            }
            var user = await _identityRepository.FindByNameAsync(tokenRequestDto.UserName);
            if (user is null || !await _identityRepository.CheckPasswordAsync(user, tokenRequestDto.Password))
            {
                authModel.Message = "Emial or Password is incorrect!!";
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _identityRepository.GetRolesAsync(user);

            authModel.IsAuthentcated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //authModel.ExpirasOn = jwtSecurityToken.ValidTo;
            authModel.Roles = roleList.ToList();
            var activeRefreshTokens = await _identityRepository.GetRefreshTokensAsync(user.Id);
            //var activeRefreshTokens = user.RefreshTokens.Any(rt => rt.IsActive);
            if (activeRefreshTokens is not null)
            {
                var activeRefreshToken = activeRefreshTokens.FirstOrDefault(rt => rt.IsActive);
                if (activeRefreshToken is null)
                {
                    var refreshToken = GenerateRefreshToken(user);
                    authModel.RefreshToken = refreshToken.Token;
                    authModel.RefreshTokenExpirasOn = refreshToken.ExpiresOn;
                    await _identityRepository.AddRefreshTokenAsync(refreshToken);
                }
                else
                {

                    authModel.RefreshToken = activeRefreshToken.Token;
                    authModel.RefreshTokenExpirasOn = activeRefreshToken.ExpiresOn;
                }

            }
            else
            {
                var refreshToken = GenerateRefreshToken(user);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpirasOn = refreshToken.ExpiresOn;
               await _identityRepository.AddRefreshTokenAsync(refreshToken);
            }

            return authModel;
        }
        public async Task<AuthDto> RefreshTokenAsync(string Token)
        {
            var refreshToken = await _identityRepository.GetRefreshTokenByNameAsync(Token);
            if (refreshToken is null)
                return new AuthDto { Message = "Invalid Token" };
            if (!refreshToken.IsActive)
                return new AuthDto { Message = "Token Expired" };


          var result= await _identityRepository.RevokeRefreshTokenAsync(refreshToken);
            if (result == 0)
                return new AuthDto { Message = "Something went wrong" };

            var refreshTokenNew = GenerateRefreshToken(refreshToken.User);
           await _identityRepository.AddRefreshTokenAsync(refreshTokenNew);
            

            var user = refreshToken.User;
            var jwtSecurityToken = await CreateJwtToken(user);
            var roleList = await _identityRepository.GetRolesAsync(user);
            var authModel = new AuthDto
            {
                IsAuthentcated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName,
                Roles = roleList.ToList(),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpirasOn = refreshToken.ExpiresOn
            };
            return authModel;
        }
        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {


            var userbyEmail = await _identityRepository.FindByEmailAsync(registerDto.Email);
            if (userbyEmail is not null)
                return  new AuthDto { Message = "This Email Is registred!!" };
            var userbyName = await _identityRepository.FindByNameAsync(registerDto.Email);
            if (userbyName is not null)
                return new AuthDto { Message = "This Username Is registred!!" };
            var User = new ApplicationUser
            {
                UserName = registerDto.Name,
                Email = registerDto.Email,

            };
            var result = await _identityRepository.CreateAsync(User, registerDto.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += error.Description + " ,";
                }
                return new AuthDto { Message = errors };
            }
            await _identityRepository.AddToRoleAsync(User, registerDto.Role);

            var jwtSecurityToken = await CreateJwtToken(User);

            return new AuthDto
            {
                Email = User.Email,
                //ExpirasOn = jwtSecurityToken.ValidTo,
                IsAuthentcated = true,
                Roles = new List<string> { registerDto.Role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = User.UserName,
            };
        }
        public async Task<bool> RevokeTokenAsync(string Token)
        {
            var refreshToken = await _identityRepository.GetRefreshTokenByNameAsync(Token);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;


            await _identityRepository.RevokeRefreshTokenAsync(refreshToken);
            

            return true;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user) {
            if (user is not null)
            {
                var userClims = await _identityRepository.GetClaimsAsync(user);
                var roles = await _identityRepository.GetRolesAsync(user);
                var roleClaims = new List<Claim>();

                foreach (var role in roles)
                {
                    roleClaims.Add(new Claim("roles", role));
                }
                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
                }
            .Union(userClims)
        .Union(roleClaims);

                if (string.IsNullOrEmpty(_jwt.Key))
                {
                    throw new ArgumentNullException(nameof(_jwt.Key), "JWT Key cannot be null or empty.");
                }

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                    signingCredentials: signingCredentials);

                return jwtSecurityToken;
            }
            throw new ArgumentNullException(nameof(_jwt.Key), "JWT Key cannot be null or empty .");
        }

        private RefreshToken GenerateRefreshToken(ApplicationUser user)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddMinutes(2),
                CreatedOn = DateTime.UtcNow,
                UserId = user.Id
            };

        }
    }
}
