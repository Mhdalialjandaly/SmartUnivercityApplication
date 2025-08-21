using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface ILectureServices
    {
        Task<List<LectureDto>> GetAllLecturesAsync();
        Task<List<LectureDto>> GetLecturesByCourseAsync(int courseId);
        Task<List<LectureDto>> GetLecturesByDateAsync(DateTime date);
        Task<List<LectureDto>> GetLecturesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<LectureDto?> GetLectureByIdAsync(int id);
        Task<LectureDto> CreateLectureAsync(LectureDto lectureDto);
        Task UpdateLectureAsync(int id, LectureDto lectureDto);
        Task DeleteLectureAsync(int id);
        Task<bool> LectureExistsAsync(int id);
        Task<LectureDto> CancelLectureAsync(int id);
        Task<LectureDto> RescheduleLectureAsync(int id, DateTime newDate, DateTime newStartTime, DateTime newEndTime);
        Task<List<LectureDto>> GetUpcomingLecturesAsync(int count);
        Task<List<LectureDto>> GetCancelledLecturesAsync();
        //Task<LectureSearchResult> GetLecturesAsync(int pageNumber, int pageSize, DateTime? date, int? courseId);
        Task<LectureScheduleDto> GetLectureScheduleAsync(DateTime date);
        Task<List<LectureScheduleDto>> GetWeeklyScheduleAsync(DateTime startDate);
        Task<int> GetLectureAttendanceCountAsync(int lectureId);
        Task<int> GetPresentStudentsCountAsync(int lectureId);
        Task<int> GetAbsentStudentsCountAsync(int lectureId);
        Task<bool> CheckLectureConflictAsync(int courseId, DateTime lectureDate, DateTime startTime, DateTime endTime);
        Task<List<LectureDto>> GetLecturesByInstructorAsync(string instructorName);
        Task<List<LectureDto>> GetLecturesByLocationAsync(string location);
    }
}
