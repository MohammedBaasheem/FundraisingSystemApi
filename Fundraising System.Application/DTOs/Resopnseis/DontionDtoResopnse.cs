using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Resopnseis
{
    public class DontionDtoResopnse
    {
       
        public int Id { get; set; }
        
        public string DonorId { get; set; } // معرف المتبرع (من جدول المتبرعين)
      
        public decimal Amount { get; set; } // المبلغ المتبرع به
    
        public DateTime DonationDate { get; set; } // تاريخ التبرع
   
        public int ProjectId { get; set; } // معرف المشروع الذي ينتمي إليه التبرع
       
    }
}
