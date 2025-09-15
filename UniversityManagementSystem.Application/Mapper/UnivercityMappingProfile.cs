using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.Mapper
{
    public class UnivercityMappingProfile :Profile
    {
        public UnivercityMappingProfile()
        {
            CreateMap<StudentDto, Student>().PreserveReferences();
            CreateMap<Student, StudentDto>();

            CreateMap<DepartmentDto, Department>().PreserveReferences();
            CreateMap<Department, DepartmentDto>();


            CreateMap<CourseDto, Course>().PreserveReferences();
            CreateMap<Course, CourseDto>();

            CreateMap<LectureDto, Lecture>().PreserveReferences();
            CreateMap<Lecture, LectureDto>();

            CreateMap<CourseRegistrationDto, CourseRegistration>().PreserveReferences();
            CreateMap<CourseRegistration, CourseRegistrationDto>();

            CreateMap<DocumentFieldDto, DocumentField>().PreserveReferences();
            CreateMap<DocumentField, DocumentFieldDto>();

            CreateMap<DocumentSignatureDto, DocumentSignature>().PreserveReferences();
            CreateMap<DocumentSignature, DocumentSignatureDto>();

            CreateMap<DocumentTemplateDto, DocumentTemplate>().PreserveReferences();
            CreateMap<DocumentTemplate, DocumentTemplateDto>();

            CreateMap<OfficialDocumentDto, OfficialDocument>().PreserveReferences();
            CreateMap<OfficialDocument, OfficialDocumentDto>();

            CreateMap<StudentApplicationDto, StudentApplication>().PreserveReferences();
            CreateMap<StudentApplication, StudentApplicationDto>();

            CreateMap<StudentPaymentDto, StudentPayment>().PreserveReferences();
            CreateMap<StudentPayment, StudentPaymentDto>();

            CreateMap<UserDto, User>().PreserveReferences();
            CreateMap<User, UserDto>();

            CreateMap<EmployeeDto, Employee>().PreserveReferences();
            CreateMap<Employee, EmployeeDto>();

            CreateMap<FinanceRecordDto, FinanceRecord>().PreserveReferences();
            CreateMap<FinanceRecord, FinanceRecordDto>();


            //CreateMap<AcademicCalendar, AcademicCalendarDto>()
            //    .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            //    .ReverseMap();
            CreateMap<AcademicCalendarDto, AcademicCalendar>().PreserveReferences();
            CreateMap<AcademicCalendar, AcademicCalendarDto>();

            CreateMap<ProfessorDto, Professor>().PreserveReferences();
            CreateMap<Professor, ProfessorDto>();


            CreateMap<StudentAttendanceDto, StudentAttendance>().PreserveReferences();
            CreateMap<StudentAttendance, StudentAttendanceDto>();

            CreateMap<ArchiveItemDto, ArchiveItem>().ReverseMap().PreserveReferences();
            CreateMap<ArchiveItem, ArchiveItemDto>().ReverseMap();

            CreateMap<NationalityDto, Nationality>().ReverseMap().PreserveReferences();
            CreateMap<Nationality, NationalityDto>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap().PreserveReferences();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<ConversationDto, Conversation>().ReverseMap().PreserveReferences();
            CreateMap<Conversation, ConversationDto>().ReverseMap(); 
            
            CreateMap<MessageDto, Message>().ReverseMap().PreserveReferences();
            CreateMap<Message, MessageDto>().ReverseMap();

            CreateMap<AssessmentDto, Assessment>().ReverseMap().PreserveReferences();
            CreateMap<Assessment, AssessmentDto>().ReverseMap();
            
            CreateMap<StudentAssessmentDto, StudentAssessment>().ReverseMap().PreserveReferences();
            CreateMap<StudentAssessment, StudentAssessmentDto>().ReverseMap();
            
            CreateMap<AccountingEntryDto, AccountingEntry>().ReverseMap().PreserveReferences();
            CreateMap<AccountingEntry, AccountingEntryDto>().ReverseMap();
            
            CreateMap<TrialBalanceAccountDto, Account>().ReverseMap().PreserveReferences();
            CreateMap<Account, TrialBalanceAccountDto>().ReverseMap();

            CreateMap<CashFundDto, CashFund>().ReverseMap().PreserveReferences();
            CreateMap<CashFund, CashFundDto>().ReverseMap();

            CreateMap<CashTransactionDto, CashTransaction>().ReverseMap().PreserveReferences();
            CreateMap<CashTransaction, CashTransactionDto>().ReverseMap();

            CreateMap<AssetCategoryDto, AssetCategory>().ReverseMap().PreserveReferences();
            CreateMap<AssetCategory, AssetCategoryDto>().ReverseMap();

            CreateMap<AssetMaintenanceDto, AssetMaintenance>().ReverseMap().PreserveReferences();
            CreateMap<AssetMaintenance, AssetMaintenanceDto>().ReverseMap();

            CreateMap<FixedAssetDto, FixedAsset>().ReverseMap().PreserveReferences();
            CreateMap<FixedAsset, FixedAssetDto>().ReverseMap();
            
            CreateMap<EmployeeSalaryDto, EmployeeSalary>().ReverseMap().PreserveReferences();
            CreateMap<EmployeeSalary, EmployeeSalaryDto>().ReverseMap();

            CreateMap<SalaryPaymentDto, SalaryPayment>().ReverseMap().PreserveReferences();
            CreateMap<SalaryPayment, SalaryPaymentDto>().ReverseMap();

            CreateMap<SalaryDeductionDto, SalaryDeduction>().ReverseMap().PreserveReferences();
            CreateMap<SalaryDeduction, SalaryDeductionDto>().ReverseMap();

            CreateMap<StudentInstallmentDto, StudentInstallment>().ReverseMap().PreserveReferences();
            CreateMap<StudentInstallment, StudentInstallmentDto>().ReverseMap();


            CreateMap<InstallmentPaymentDto, InstallmentPayment>().ReverseMap().PreserveReferences();
            CreateMap<InstallmentPayment, InstallmentPaymentDto>().ReverseMap();


        }
    }
}
