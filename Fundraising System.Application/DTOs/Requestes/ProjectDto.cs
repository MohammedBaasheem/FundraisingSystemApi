using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Requestes
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; } // اسم المشروع
        [Required]
        public string Description { get; set; } // وصف المشروع
        [Required]
        public decimal FinancialGoal { get; set; } // الهدف المالي للمشروع

        //[Required]

        // خاصية لمجموع التبرعات الحالية
        public decimal CurrentTotalDonations { get; set; } // مجموع التبرعات الخاص بالمشروع
    }
}
