using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IAcademicCalendarServices
    {
        Task<List<AcademicCalendarDto>> GetEventsByDateRangeAsync(DateTime start, DateTime end);
        Task<List<AcademicCalendarDto>> GetEventsBySemesterAsync(int semester, int academicYear);
        Task<AcademicCalendarDto> GetEventByIdAsync(int id);
        Task CreateEventAsync(AcademicCalendarDto calendarEvent);
        Task UpdateEventAsync(int id, AcademicCalendarDto calendarEvent);
        Task DeleteEventAsync(int id);
        Task<List<AcademicCalendarDto>> GetHolidaysAsync(int academicYear);
        Task<IEnumerable<AcademicCalendarDto>> GetDepartmentEventsAsync(int departmentId, int academicYear);
        Task<IEnumerable<AcademicCalendarDto>> GetImportantDatesAsync(int academicYear);
        Task<List<AcademicCalendarDto>> GetUpcomingEventsAsync(int daysAhead);
        Task<bool> EventExistsAsync(int id);
    }
}
