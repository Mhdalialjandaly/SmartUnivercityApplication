

namespace UniversityManagementSystem.Application.DTOs
{
    public class EquityStatementDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime GeneratedAt { get; set; }
        public List<EquityStatementItemDto> EquityItems { get; set; } = new List<EquityStatementItemDto>();
        public decimal TotalEquityChange { get; set; }
    }
}
