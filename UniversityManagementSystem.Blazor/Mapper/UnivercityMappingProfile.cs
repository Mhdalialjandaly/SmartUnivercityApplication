using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Blazor.Components.Pages;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Blazor.Mapper
{
    public class UnivercityMappingProfile : Profile
    {
        public UnivercityMappingProfile()
        {
            CreateMap<StudentDto, Student>().PreserveReferences();
            CreateMap<Student, StudentDto>();

            CreateMap<DepartmentDto, Department>().PreserveReferences();
            CreateMap<Department, DepartmentDto>();

            CreateMap<AuthDto, Auth>().PreserveReferences();
            CreateMap<Auth, AuthDto>();

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

        }
    }
}
