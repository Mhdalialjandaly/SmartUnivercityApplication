using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class Student : User , IEntity
    {
        public Student() {
            CourseRegistrations = new HashSet<CourseRegistration>();
            Universities = new HashSet<University>();
            StudentDocuments = new HashSet<StudentDocument>();
        }
        public string StudentId { get; set; } = "";    
        public string Phone { get; set; } = "";  
        public int DepartmentId { get; set; }
        public decimal AccountBalance { get; set; } = 0;
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
        public string StatusOnSite { get; set; } = "نشط"; // نشط, موقوف, متخرج
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

        // العلاقات
        public virtual Department Department { get; set; }
        public virtual Tunnel Tunnel { get; set; }
        public virtual ICollection<University> Universities { get; set; }
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }
        public virtual ICollection<StudentDocument> StudentDocuments { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
