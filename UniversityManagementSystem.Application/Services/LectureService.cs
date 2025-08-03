using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class LectureService : Injectable, ILectureService
    {
        private readonly IRepository<Lecture> _lectureRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Attendance> _attendanceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LectureService(
            IRepository<Lecture> lectureRepository,
            IRepository<Course> courseRepository,
            IRepository<Attendance> attendanceRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _lectureRepository = lectureRepository;
            _courseRepository = courseRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LectureDto>> GetAllLecturesAsync()
        {
            var lectures = await _lectureRepository.GetAllAsync(l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures);
        }

        public async Task<List<LectureDto>> GetLecturesByCourseAsync(int courseId)
        {
            var lectures = await _lectureRepository.GetAllAsync(l => l.CourseId == courseId, l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures);
        }

        public async Task<List<LectureDto>> GetLecturesByDateAsync(DateTime date)
        {
            var lectures = await _lectureRepository.GetAllAsync(
                l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures.Where(e=>e.LectureDate.Date == date.Date));
        }

        public async Task<List<LectureDto>> GetLecturesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var lectures = await _lectureRepository.GetAllAsync(
                l => l.LectureDate >= startDate && l.LectureDate <= endDate,
                l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures);
        }

        public async Task<LectureDto?> GetLectureByIdAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id, l => l.Course);
            return lecture != null ? _mapper.Map<LectureDto>(lecture) : null;
        }

        public async Task<LectureDto> CreateLectureAsync(LectureDto lectureDto)
        {
            var lecture = _mapper.Map<Lecture>(lectureDto);
            lecture.CreatedDate = DateTime.Now;

            await _lectureRepository.AddAsync(lecture);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LectureDto>(lecture);
        }

        public async Task UpdateLectureAsync(int id, LectureDto lectureDto)
        {
            var existingLecture = await _lectureRepository.GetByIdAsync(id);
            if (existingLecture == null)
                throw new Exception($"Lecture with ID {id} not found");

            _mapper.Map(lectureDto, existingLecture);
            _lectureRepository.Update(existingLecture);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteLectureAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
                throw new Exception($"Lecture with ID {id} not found");

            _lectureRepository.Delete(lecture);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> LectureExistsAsync(int id)
        {
            return await _lectureRepository.ExistsAsync(l => l.Id == id);
        }

        public async Task<LectureDto> CancelLectureAsync(int id)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
                throw new Exception($"Lecture with ID {id} not found");

            lecture.IsCancelled = true;
            _lectureRepository.Update(lecture);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LectureDto>(lecture);
        }

        public async Task<LectureDto> RescheduleLectureAsync(int id, DateTime newDate, DateTime newStartTime, DateTime newEndTime)
        {
            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null)
                throw new Exception($"Lecture with ID {id} not found");

            lecture.LectureDate = newDate;
            lecture.StartTime = newStartTime;
            lecture.EndTime = newEndTime;
            lecture.IsCancelled = false; // إعادة تفعيل المحاضرة عند إعادة جدولتها

            _lectureRepository.Update(lecture);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LectureDto>(lecture);
        }

        public async Task<List<LectureDto>> GetUpcomingLecturesAsync(int count)
        {
            var lectures = await _lectureRepository.GetAllAsync(l => l.Course);
            var upcomingLectures = lectures
                .Where(l => l.LectureDate >= DateTime.Now && !l.IsCancelled)
                .OrderBy(l => l.LectureDate)
                .ThenBy(l => l.StartTime)
                .Take(count);

            return _mapper.Map<List<LectureDto>>(upcomingLectures);
        }

        public async Task<List<LectureDto>> GetCancelledLecturesAsync()
        {
            var lectures = await _lectureRepository.GetAllAsync(l => l.IsCancelled, l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures);
        }

        //public async Task<LectureSearchResult> GetLecturesAsync(int pageNumber, int pageSize, DateTime? date, int? courseId)
        //{
        //    var query = _lectureRepository.GetAllAsync();

        //    // تطبيق الفلترة حسب التاريخ
        //    if (date.HasValue)
        //    {
        //        query = query.Where(l => l.LectureDate.Date == date.Value.Date);
        //    }

        //    // تطبيق الفلترة حسب المادة
        //    if (courseId.HasValue)
        //    {
        //        query = query.Where(l => l.CourseId == courseId.Value);
        //    }

        //    var totalCount = await query.CountAsync();
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        //    var lectures = await query
        //        .Include(l => l.Course)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return new LectureSearchResult
        //    {
        //        Lectures = _mapper.Map<List<LectureDto>>(lectures),
        //        TotalCount = totalCount,
        //        TotalPages = totalPages
        //    };
        //}

        public async Task<LectureScheduleDto> GetLectureScheduleAsync(DateTime date)
        {
            var lectures = await _lectureRepository.GetAllAsync(
                l => l.LectureDate.Date == date.Date && !l.IsCancelled,
                l => l.Course);

            return new LectureScheduleDto
            {
                Date = date,
                Lectures = _mapper.Map<List<LectureDto>>(lectures.OrderBy(l => l.StartTime))
            };
        }

        public async Task<List<LectureScheduleDto>> GetWeeklyScheduleAsync(DateTime startDate)
        {
            var endDate = startDate.AddDays(7);
            var lectures = await _lectureRepository.GetAllAsync(
                l => l.LectureDate >= startDate && l.LectureDate <= endDate && !l.IsCancelled,
                l => l.Course);

            var schedule = new List<LectureScheduleDto>();
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                var dailyLectures = lectures
                    .Where(l => l.LectureDate.Date == date)
                    .OrderBy(l => l.StartTime)
                    .ToList();

                schedule.Add(new LectureScheduleDto
                {
                    Date = date,
                    Lectures = _mapper.Map<List<LectureDto>>(dailyLectures)
                });
            }

            return schedule;
        }

        public async Task<int> GetLectureAttendanceCountAsync(int lectureId)
        {
            return await _attendanceRepository.CountAsync(a => a.LectureId == lectureId);
        }

        public async Task<int> GetPresentStudentsCountAsync(int lectureId)
        {
            return await _attendanceRepository.CountAsync(a => a.LectureId == lectureId && a.Status == "حاضر");
        }

        public async Task<int> GetAbsentStudentsCountAsync(int lectureId)
        {
            return await _attendanceRepository.CountAsync(a => a.LectureId == lectureId && a.Status == "غائب");
        }

        public async Task<bool> CheckLectureConflictAsync(int courseId, DateTime lectureDate, DateTime startTime, DateTime endTime)
        {
            var conflictingLectures = await _lectureRepository.GetAllAsync(
                l => l.CourseId == courseId &&
                     l.LectureDate.Date == lectureDate.Date &&
                     !l.IsCancelled &&
                     ((l.StartTime <= startTime && l.EndTime > startTime) ||
                      (l.StartTime < endTime && l.EndTime >= endTime) ||
                      (l.StartTime >= startTime && l.EndTime <= endTime)));

            return conflictingLectures.Any();
        }

        public async Task<List<LectureDto>> GetLecturesByInstructorAsync(string instructorName)
        {
            var lectures = await _lectureRepository.GetAllAsync(l => l.Course);
            var instructorLectures = lectures.Where(l => l.Course?.Instructor.Contains(instructorName) == true);
            return _mapper.Map<List<LectureDto>>(instructorLectures);
        }

        public async Task<List<LectureDto>> GetLecturesByLocationAsync(string location)
        {
            var lectures = await _lectureRepository.GetAllAsync(
                l => l.Location.Contains(location),
                l => l.Course);
            return _mapper.Map<List<LectureDto>>(lectures);
        }
    }
}
