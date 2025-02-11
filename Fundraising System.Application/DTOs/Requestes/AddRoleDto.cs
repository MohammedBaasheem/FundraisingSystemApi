using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Resopnseis
{
    public class AddRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
