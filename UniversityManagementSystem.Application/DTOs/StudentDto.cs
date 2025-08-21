using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentDto
    {     
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public string PasswordHash { get; set; }
        public decimal AccountBalance { get; set; }
        public string HomeAddress { get; set; }
        public string Religion { get; set; }
        public string SecoundPhone { get; set; }
        public bool TrueIsEmployee { get; set; }
        public string WorkAddress { get; set; }
        public DateTime BirthOfDate { get; set; }
        public bool Sexual { get; set; }
        public string PoliticalNationalism { get; set; }
        public string NationalityName { get; set; }
        public int HomeNumber { get; set; }
        public string CivilstatusIDNumberAndNationalCard { get; set; }
        public string CivilstatusIDNumberAndNationalCardFrom { get; set; }
        public DateTime CivilstatusIDNumberAndNationalCardDate { get; set; }
        public int NationalityCertificateNumber { get; set; }
        public string NationalityCertificateNumberFrom { get; set; }
        public DateTime NationalityCertificateNumberDate { get; set; }
        public string UserNameOnSite { get; set; }
        public string PasswordOnSite { get; set; }
        public string StatusOnSite { get; set; }
        public StudentStatus Status { get; set; }
        public int ExamNumber { get; set; }
        public int SecretNumber { get; set; }
        public bool ApplyTunnel { get; set; }
        public int TunnelId { get; set; }
        public bool ERegistrationCompleted { get; set; }
        public bool RegistraionCompleted { get; set; }
        public bool SubmissionIsCompleted { get; set; }
        public string SubmissionDepartment { get; set; }
        public string TunnelDepartment { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int AccountsReceiptNumber { get; set; }
        public string SchoolName { get; set; }
        public string Branch { get; set; }
        public DateTime DateOfSchoolGraduate { get; set; }
        public int StudentTotalWithoutAdditions { get; set; }
        public int StudentTotalWithAdditions { get; set; }
        public int StudentGPAWithoutAdditions { get; set; }
        public int StudentGPAWithAdditions { get; set; }
        public double GPA { get; set; }
        public string FullAddress { get; set; }
        public string AcademicYear { get; set; }
        public int NationalityId { get; set; }
        // العلاقات
        public DepartmentDto Department { get; set; }
        public TunnelDto Tunnel { get; set; }
        public NationalityDto Nationality { get; set; }
        public List<CourseRegistrationDto> CourseRegistrations { get; set; }
        public List<StudentDocumentDto> StudentDocuments { get; set; }
        public List<AttendanceDto> Attendances { get; set; }
        public List<StudentApplicationDto> StudentApplications { get; set; }
        public List<StudentPaymentDto> StudentPayments { get; set; }
        public List<StudentAttendanceDto> StudentAttendances { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
