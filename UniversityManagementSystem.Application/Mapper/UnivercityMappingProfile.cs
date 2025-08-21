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
        }
    }
}
