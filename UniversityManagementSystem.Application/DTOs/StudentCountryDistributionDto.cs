
namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentCountryDistributionDto
    {
        public string CountryCode { get; set; } // كود الدولة (مثل SA, EG, US)
        public string CountryName { get; set; } // اسم الدولة بالكامل
        public int StudentCount { get; set; }   // عدد الطلاب من هذه الدولة
        public double Percentage { get; set; }  // النسبة المئوية من إجمالي الطلاب
    }
}
