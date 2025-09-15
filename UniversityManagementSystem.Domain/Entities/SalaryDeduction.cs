
namespace UniversityManagementSystem.Domain.Entities
{
    public class SalaryDeduction : IEntity
    {
        public int Id { get; set; }
        public int EmployeeSalaryId { get; set; }
        public string DeductionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DeductionDate { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
