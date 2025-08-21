using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentAttendanceDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string AbsenceReason { get; set; }
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime RecordedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string RecordedBy { get; set; }
        public int DepartmentId { get; set; }
        public CourseDto CourseDto { get; set; }
    }   

   
}
   
