using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class AcademicCalendarServices :Injectable ,IAcademicCalendarServices
    {
        private readonly IRepository<AcademicCalendar> _calendarRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AcademicCalendarServices(IRepository<AcademicCalendar> calendarRepository, 
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _calendarRepository = calendarRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AcademicCalendarDto>> GetEventsByDateRangeAsync(DateTime start, DateTime end)
        {
            var events = await _calendarRepository.GetAllAsync(e => e.Department);

            return _mapper.Map<List<AcademicCalendarDto>>(events);
        }

        public async Task<List<AcademicCalendarDto>> GetEventsBySemesterAsync(int semester, int academicYear)
        {
            DateTime semesterStart, semesterEnd;

            // تحديد تواريخ الفصل الدراسي (يمكن تعديلها حسب نظام الجامعة)
            switch (semester)
            {
                case 1: // الفصل الأول
                    semesterStart = new DateTime(academicYear, 9, 1);
                    semesterEnd = new DateTime(academicYear, 12, 31);
                    break;
                case 2: // الفصل الثاني
                    semesterStart = new DateTime(academicYear, 1, 1);
                    semesterEnd = new DateTime(academicYear, 4, 30);
                    break;
                case 3: // الفصل الصيفي
                    semesterStart = new DateTime(academicYear, 5, 1);
                    semesterEnd = new DateTime(academicYear, 8, 31);
                    break;
                default:
                    throw new ArgumentException("الفصل الدراسي غير صحيح");
            }

            return await GetEventsByDateRangeAsync(semesterStart, semesterEnd);
        }

        public async Task<AcademicCalendarDto> GetEventByIdAsync(int id)
        {
            var calendarEvent = await _calendarRepository.GetByIdAsync(id,e => e.Department);

            if (calendarEvent == null)
                throw new KeyNotFoundException("الحدث غير موجود");

            return _mapper.Map<AcademicCalendarDto>(calendarEvent);
        }

        public async Task CreateEventAsync(AcademicCalendarDto calendarEvent)
        {
            ValidateEventDates(calendarEvent.StartDate, calendarEvent.EndDate);

            var entity = _mapper.Map<AcademicCalendar>(calendarEvent);
            await _calendarRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateEventAsync(int id, AcademicCalendarDto calendarEvent)
        {
            ValidateEventDates(calendarEvent.StartDate, calendarEvent.EndDate);

            var existingEvent = await _calendarRepository.GetByIdAsync(id);
            if (existingEvent == null)
                throw new KeyNotFoundException("الحدث غير موجود");

            _mapper.Map(calendarEvent, existingEvent);
            _calendarRepository.Update(existingEvent);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var calendarEvent = await _calendarRepository.GetByIdAsync(id);
            if (calendarEvent == null)
                throw new KeyNotFoundException("الحدث غير موجود");

            _calendarRepository.Delete(calendarEvent);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<AcademicCalendarDto>> GetHolidaysAsync(int academicYear)
        {
            var holidays = await _calendarRepository.GetAllAsync(e => e.IsHoliday && e.AcademicYear == academicYear);

            return _mapper.Map<List<AcademicCalendarDto>>(holidays);
        }

        public async Task<IEnumerable<AcademicCalendarDto>> GetDepartmentEventsAsync(int departmentId, int academicYear)
        {
            var events = await _calendarRepository.GetAllAsync(e => e.Department);

            return _mapper.Map<List<AcademicCalendarDto>>(events).Where(e => e.DepartmentId == departmentId && e.AcademicYear == academicYear);
        }

        public async Task<IEnumerable<AcademicCalendarDto>> GetImportantDatesAsync(int academicYear)
        {
            var importantDates = await _calendarRepository.GetAllAsync();

            return _mapper.Map<List<AcademicCalendarDto>>(importantDates).Where(e => e.AcademicYear == academicYear &&
                           (e.EventType == CalendarEventType.Exam ||
                            e.EventType == CalendarEventType.Registration)).OrderBy(e => e.StartDate);
        }

        public async Task<List<AcademicCalendarDto>> GetUpcomingEventsAsync(int daysAhead)
        {
            var today = DateTime.Today;
            var endDate = today.AddDays(daysAhead);

            var events = await _calendarRepository.GetAllAsync(e => e.Department);

            return _mapper.Map<List<AcademicCalendarDto>>(events).Where(e => e.StartDate >= today && e.StartDate <= endDate)
                .OrderBy(e => e.StartDate).ToList();
        }

        public async Task<bool> EventExistsAsync(int id)
        { 
            var result = await _calendarRepository.GetAllAsync();
            return result.Any(e => e.Id == id);
        }

        private void ValidateEventDates(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("تاريخ النهاية لا يمكن أن يكون قبل تاريخ البداية");

            if (startDate < DateTime.Today.AddYears(-1))
                throw new ArgumentException("لا يمكن إضافة أحداث لأكثر من سنة مضت");
        }
    }
}
