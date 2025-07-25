using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Domain.Entities;

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

        }
    }
}
