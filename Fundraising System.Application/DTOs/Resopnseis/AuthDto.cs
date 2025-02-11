using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Requestes
{
    public class AuthDto
    {
        public string Message { get; set; }
        public bool IsAuthentcated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        //public DateTime ExpirasOn { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirasOn { get; set; }
    }
}
