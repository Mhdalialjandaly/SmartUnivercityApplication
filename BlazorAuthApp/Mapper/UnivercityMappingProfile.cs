using AutoMapper;
using BlazorAuthApp.Components.Pages;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;

namespace BlazorAuthApp.Mapper
{
    public class UnivercityMappingProfile : Profile
    {
        public UnivercityMappingProfile() 
        {
            CreateMap<StudentDto,Student>().PreserveReferences();
            CreateMap<Student, StudentDto>();

            CreateMap<DepartmentDto, Department>().PreserveReferences();
            CreateMap<Department, DepartmentDto>();

            CreateMap<AuthDto, Auth>().PreserveReferences();
            CreateMap<Auth, AuthDto>();

            CreateMap<CourseDto, Course>().PreserveReferences();
            CreateMap<Course, CourseDto>();

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

        }
    }
}
