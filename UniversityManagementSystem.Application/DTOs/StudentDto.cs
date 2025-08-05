using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public decimal AccountBalance { get; set; }
        public string HomeAddress { get; set; }
        public string Religion { get; set; }
        public string SecoundPhone { get; set; }
        public bool TrueIsEmployee { get; set; }
        public string WorkAddress { get; set; }
        public DateTime BirthOfDate { get; set; }
        public bool Sexual { get; set; }
        public string PoliticalNationalism { get; set; }
        public string Nationality { get; set; }
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
        public string Image { get; set; }

        // العلاقات
        public DepartmentDto Department { get; set; }
        public Tunnel Tunnel { get; set; }
        public List<University> Universities { get; set; }
        public List<CourseRegistration> CourseRegistrations { get; set; }
        public List<StudentDocument> StudentDocuments { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
}
