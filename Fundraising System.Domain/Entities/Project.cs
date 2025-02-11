using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; } // معرف فريد للمشروع
        public string Name { get; set; } // اسم المشروع
        public string Description { get; set; } // وصف المشروع
        public decimal FinancialGoal { get; set; } // الهدف المالي للمشروع

        public ICollection<Donation> Donations { get; set; } // علاقة مع التبرعات

        // خاصية لمجموع التبرعات الحالية
        public decimal CurrentTotalDonations { get; set; } // مجموع التبرعات الخاص بالمشروع
    }

}
