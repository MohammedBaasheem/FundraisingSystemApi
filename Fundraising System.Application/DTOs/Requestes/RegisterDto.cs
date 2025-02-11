using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Resopnseis
{
    public class RegisterDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }=string.Empty;
        [Required, StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Role { get; set; }
    }
}
