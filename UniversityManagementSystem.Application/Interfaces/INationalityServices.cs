using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface INationalityServices
    {
        Task<List<NationalityDto>> GetAllNationalitiesAsync();
        Task<NationalityDto?> GetNationalityByIdAsync(int id);
        Task<NationalityDto?> GetNationalityByCountryCodeAsync(string countryCode);
        Task<NationalityDto> CreateNationalityAsync(NationalityDto nationalityDto);
        Task UpdateNationalityAsync(int id, NationalityDto nationalityDto);
        Task DeleteNationalityAsync(int id);
        Task<bool> NationalityExistsAsync(int id);
        Task<bool> CountryCodeExistsAsync(string countryCode);
        Task<List<UserDto>> GetUsersByNationalityAsync(int nationalityId);
        Task<int> GetUserCountByNationalityAsync(int nationalityId);
    }
}
