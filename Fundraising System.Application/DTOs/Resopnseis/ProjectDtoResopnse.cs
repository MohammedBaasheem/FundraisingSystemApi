using Fundraising_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundraising_System.Application.DTOs.Resopnseis
{
    public class ProjectDtoResopnse
    {
        public int Id { get; set; } // معرف فريد للمشروع
        public string Name { get; set; } // اسم المشروع
        public string Description { get; set; } // وصف المشروع
        public decimal FinancialGoal { get; set; } // الهدف المالي للمشروع

        public ICollection<DontionDtoResopnse> Donations { get; set; } // علاقة مع التبرعات

        // خاصية لمجموع التبرعات الحالية
        public decimal CurrentTotalDonations { get; set; } // مجموع التبرعات الخاص بالمشروع
    }
}
