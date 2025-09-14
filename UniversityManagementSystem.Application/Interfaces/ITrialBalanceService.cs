using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ITrialBalanceService
    {
        Task<TrialBalanceDto> GenerateTrialBalanceAsync(DateTime fromDate, DateTime toDate);
        Task<List<TrialBalanceAccountDto>> GetTrialBalanceAccountsAsync(DateTime fromDate, DateTime toDate);
        Task<TrialBalanceSummaryDto> GetTrialBalanceSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportTrialBalanceToExcelAsync(DateTime fromDate, DateTime toDate);
        Task<byte[]> ExportTrialBalanceToPdfAsync(DateTime fromDate, DateTime toDate);
        Task<List<TrialBalanceAccountDto>> GetTrialBalanceByAccountTypeAsync(DateTime fromDate, DateTime toDate, string accountType);
        Task<decimal> GetAccountBalanceAsync(string accountCode, DateTime fromDate, DateTime toDate);
    }
}
