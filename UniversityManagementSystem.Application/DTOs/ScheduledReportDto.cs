using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.DTOs
{
    public class ScheduledReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // monthly, quarterly, annual
        public int DayOfMonth { get; set; }
        public DateTime Time { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastRunDate { get; set; }
        public string TransactionType { get; set; } // all, income, expense
    }
}
