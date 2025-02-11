using Fundraising_System.Application.DTOs.Requestes;
using Fundraising_System.Application.DTOs.Resopnseis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.UseCaseInterface
{
    public interface IIdentityService
    {

        Task<AuthDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthDto> GetTokenAsync(TokenRequestDto tokenRequestDto);
        Task<string> AddRloeAsync(AddRoleDto addRoleDto);
        Task<AuthDto> RefreshTokenAsync(string Token);
        Task<bool> RevokeTokenAsync(string Token);
    }
}
