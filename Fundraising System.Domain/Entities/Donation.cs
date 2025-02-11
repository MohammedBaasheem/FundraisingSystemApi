using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Domain.Entities
{
    public class Donation
    {
        public int Id { get; set; } // معرف فريد للتبرع
        public string DonorId { get; set; } // معرف المتبرع (من جدول المتبرعين)
        public decimal Amount { get; set; } // المبلغ المتبرع به
        public DateTime DonationDate { get; set; } // تاريخ التبرع

        public ApplicationUser Donor { get; set; } // علاقة مع المتبرع
        public int ProjectId { get; set; } // معرف المشروع الذي ينتمي إليه التبرع
        public Project Project { get; set; } // علاقة مع المشروع
    }

}
