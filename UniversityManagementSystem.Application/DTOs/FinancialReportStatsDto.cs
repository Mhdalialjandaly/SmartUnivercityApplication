using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.Application.DTOs
{
    public class FinancialReportStatsDto
    {
        public int TotalReports { get; set; }
        public int GeneratedReports { get; set; }
        public int ScheduledReports { get; set; }
        public int ArchivedReports { get; set; }
        public int PendingReports { get; set; }
    }
}
