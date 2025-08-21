using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IFinanceReportService
    {
        Task<PaginatedResult<FinancialReportDto>> GetReportsAsync(
            int pageNumber,
            int pageSize,
            string searchTerm = "",
            string status = "");

        Task<PaginatedResult<FinancialReportDto>> GetReportsByTypeAsync(
            string type,
            int pageNumber,
            int pageSize);

        Task<FinancialReportStatsDto> GetReportStatsAsync();

        Task<FinancialReportDto> GenerateReportAsync(FinancialReportDto reportDto);

        Task<ScheduledReportDto> ScheduleReportAsync(ScheduledReportDto scheduledReportDto);

        Task<byte[]> DownloadReportAsync(int reportId);

        Task DeleteReportAsync(int reportId);

        Task<List<ScheduledReportDto>> GetActiveScheduledReportsAsync();
    }
}
