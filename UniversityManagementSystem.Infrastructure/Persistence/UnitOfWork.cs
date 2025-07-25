using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;
using UniversityManagementSystem.Infrastructure.Data;
using UniversityManagementSystem.Infrastructure.Data.Repositories;

namespace UniversityManagementSystem.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly UniversityDbContext _context;

        public UnitOfWork(UniversityDbContext context)
        {
            _context = context;
        }

        private IRepository<Student> _studentRepository;
        public IRepository<Student> StudentRepository =>
            _studentRepository ??= new Repository<Student>(_context);

        private IRepository<Department> _departmentRepository;
        public IRepository<Department> DepartmentRepository =>
            _departmentRepository ??= new Repository<Department>(_context);

        private IRepository<Course> _courseRepository;
        public IRepository<Course> CourseRepository =>
            _courseRepository ??= new Repository<Course>(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
