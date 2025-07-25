using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Domain.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Student> StudentRepository { get; }
        IRepository<Department> DepartmentRepository { get; }
        IRepository<Course> CourseRepository { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
        void Dispose();
    }
}
