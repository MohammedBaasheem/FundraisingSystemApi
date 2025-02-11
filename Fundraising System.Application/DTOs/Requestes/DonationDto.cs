using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Resopnseis
{
    public class DonationDto
    {
        [Required]
        public string DonorId { get; set; } // معرف المتبرع
        [Required]
       
        public decimal Amount { get; set; } // المبلغ المتبرع به
        [Required]
        public DateTime DonationDate { get; set; } // تاريخ التبرع
        [Required]
        public int ProjectId { get; set; } // معرف المشروع
    }
}
