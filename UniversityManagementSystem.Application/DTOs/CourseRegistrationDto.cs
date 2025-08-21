namespace UniversityManagementSystem.Application.DTOs
{
    public class CourseRegistrationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; } 
        public StudentDto Student { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public CourseDto Course { get; set; } 
        public DateTime RegistrationDate { get; set; } 
        public string Status { get; set; } 
        public decimal AmountPaid { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public decimal CourseFee { get; set; } 
        public string DepartmentName { get; set; }
    }
}
