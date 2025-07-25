using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class StudentDto
    {
        [Display(Name = "السنة الدراسية")]
        public string AcademicYear { get; set; }
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = "اسم العائلة")]
        public string LastName { get; set; }
        [Required]
        [StringLength(20)]
        public string StudentId { get; set; } = "";

        [Required]
        [Display(Name = "رقم الهاتف الاول")]
        [StringLength(20)]
        public string Phone { get; set; } = "";
        public int DepartmentId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AccountBalance { get; set; } = 0;
        [Required]
        [Display(Name = "محل السكن  م-ز-د")]
        public string HomeAddress { get; set; }
        [Required]
        [Display(Name = "الديانة")]
        public string Religion { get; set; }
        [Display(Name = "رقم الهاتف الثاني")]
        public string SecoundPhone { get; set; }
        [Display(Name = "هل انت موظف؟")]
        public bool TrueIsEmployee { get; set; }
        [Display(Name = "مكان العمل")]
        public string WorkAddress { get; set; }
        [Display(Name = "تاريخ الولادة")]
        public DateTime BirthOfDate { get; set; }
        [Display(Name = "الجنس")]
        public bool Sexual { get; set; }
        [Display(Name = "اقومية")]
        public string PoliticalNationalism { get; set; }
        [Display(Name = "الجنسية")]
        public string Nationality { get; set; }
        [Display(Name = "رقم الدار")]
        public int HomeNumber { get; set; }
        [Display(Name = "رقم هوية الاحوال المدنية والبطاقة الوطنية")]
        public string CivilstatusIDNumberAndNationalCard { get; set; }
        [Display(Name = " هوية الاحوال المدنية والبطاقة الوطنية صادرة من")]
        public string CivilstatusIDNumberAndNationalCardFrom { get; set; }
        [Display(Name = " هوية الاحوال المدنية والبطاقة الوطنية تاريخها")]
        public DateTime CivilstatusIDNumberAndNationalCardDate { get; set; }
        [Display(Name = "رقم شهادة الجنسية")]
        public int NationalityCertificateNumber { get; set; }
        [Display(Name = "  شهادة الجنسية صادرة من")]
        public string NationalityCertificateNumberFrom { get; set; }
        [Display(Name = "  شهادة الجنسية تاريخها")]
        public DateTime NationalityCertificateNumberDate { get; set; }

        [Display(Name = "اسم المستخدم على نظام الجامعة")]
        public string UserNameOnSite { get; set; }

        [Display(Name = "كلمة سر المستخدم على نظام الجامعة")]
        public string PasswordOnSite { get; set; }
        [Display(Name = "حالة الحساب على نظام الاجامعة")]
        [StringLength(20)]
        public string StatusOnSite { get; set; } = "نشط"; // نشط, موقوف, متخرج
        public StudentStatus Status { get; set; }  // نشط, موقوف
        [Display(Name = "رقم الامتحاني")]
        public int ExamNumber { get; set; }
        [Display(Name = "رقم السري")]
        public int SecretNumber { get; set; }
        [Display(Name = "رقم السري")]
        public bool ApplyTunnel { get; set; }
        [Display(Name = "قناة القبول الخاص")]
        public int TunnelId { get; set; }
        [Display(Name = "اكتمل الحجز الالكتروني ؟")]
        public bool ERegistrationCompleted { get; set; }
        [Display(Name = "اكتمل التسجيل ؟")]
        public bool RegistraionCompleted { get; set; }
        [Display(Name = "اكتمل التقديم ؟")]
        public bool SubmissionIsCompleted { get; set; }
        [Display(Name = "قسم المسجل عليه؟")]
        public string SubmissionDepartment { get; set; }
        [Display(Name = "قناة التسجيل")]
        public string TunnelDepartment { get; set; }
        [Display(Name = "تاريخ اكتمال التسجيل")]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "رقم وصل الحسابات")]
        public int AccountsReceiptNumber { get; set; }
        [Display(Name = "اسم المدرسة")]
        public string SchoolName { get; set; }
        [Display(Name = "الفرع")]
        public string Branch { get; set; }
        [Display(Name = "تايخ التخرج من المدرسة")]
        public DateTime DateOfSchoolGraduate { get; set; }
        [Display(Name = "مجموع الطالب بدون اضافات")]
        public int StudentTotalWithoutAdditions { get; set; }
        [Display(Name = "مجموع الطالب مع اضافات")]
        public int StudentTotalWithAdditions { get; set; }
        [Display(Name = "معدل الطالب بدون اضافات")]
        public int StudentGPAWithoutAdditions { get; set; }
        [Display(Name = "معدل الطالب مع اضافات")]
        public int StudentGPAWithAdditions { get; set; }
        [Display(Name = "العناون الدائم")]
        public string FullAddress { get; set; }
        [Display(Name = "المعدل التراكمي")]
        public double GPA { get; set; }
        // العلاقات
        public virtual DepartmentDto Department { get; set; }
        public virtual TunnelDto Tunnel { get; set; }
        public IList<University> Universities { get; set; }
        public IList<CourseRegistration> CourseRegistrations { get; set; }
        public IList<StudentDocument> StudentDocuments { get; set; }
    }
}
