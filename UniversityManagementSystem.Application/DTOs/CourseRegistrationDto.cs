namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseRegistrationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "مسجل"; // مسجل, ملغي, مكتمل
        public decimal AmountPaid { get; set; } = 0;
        public DateTime PaymentDate { get; set; }
    }
}
