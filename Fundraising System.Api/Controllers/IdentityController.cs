using Fundraising_System.Application.DTOs.Resopnseis;
using Fundraising_System.Application.UseCaseInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundraising_System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _IdentityService;
        public IdentityController(IIdentityService IdentityService)
        {
            _IdentityService = IdentityService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IdentityService.RegisterAsync(model);
            if (!result.IsAuthentcated)
                return BadRequest(result.Message);
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpirasOn);
            return Ok(result);
        }
        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IdentityService.GetTokenAsync(model);
            if (!result.IsAuthentcated)
                return BadRequest(result.Message);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpirasOn);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _IdentityService.AddRloeAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok(model);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest("Invalid ...Token"+ refreshToken);
            var result = await _IdentityService.RefreshTokenAsync(refreshToken);
            if (!result.IsAuthentcated)
                return BadRequest(result.Message);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpirasOn);
            }
            return Ok(result);
        }
        [HttpPost("RevokToken")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeToken revokeToken)
        {
            var token = revokeToken.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is Required");
            }
            var result = await _IdentityService.RevokeTokenAsync(token);
            if (!result)
                return BadRequest("Token is invalid");
            return Ok();

        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            if(refreshToken is not null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = expires.ToLocalTime(),
                };
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            }
            else
            {
                 BadRequest("Token is يؤسيببللا invalid");
            }
        }
    }
}
