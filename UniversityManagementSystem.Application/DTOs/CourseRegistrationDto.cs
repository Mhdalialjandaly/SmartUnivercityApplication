namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseRegistrationDto
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = "";
        public string StudentName { get; set; } = "";
        public int CourseId { get; set; }
        public string CourseName { get; set; } = "";
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "مسجل"; // مسجل, ملغي, مكتمل
        public decimal AmountPaid { get; set; } = 0;
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal CourseFee { get; set; } = 0;
        public string DepartmentName { get; set; } = "";
    }
}
