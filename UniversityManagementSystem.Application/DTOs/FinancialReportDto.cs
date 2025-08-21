using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.DTOs
{
    public class FinancialReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // monthly, quarterly, annual, custom
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } // generated, pending, scheduled, archived
        public int? DepartmentId { get; set; }
        public string TransactionType { get; set; } // all, income, expense
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string GeneratedBy { get; set; }
        public string Period { get; set; }
    }
}
