using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class NationalityServices : Injectable, INationalityServices
    {
        private readonly IRepository<Nationality> _nationalityRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NationalityServices(
            IRepository<Nationality> nationalityRepository,
            IRepository<User> userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _nationalityRepository = nationalityRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<NationalityDto>> GetAllNationalitiesAsync()
        {
            var nationalities = await _nationalityRepository.GetAllAsync();
            return _mapper.Map<List<NationalityDto>>(nationalities);
        }

        public async Task<NationalityDto?> GetNationalityByIdAsync(int id)
        {
            var nationality = await _nationalityRepository.GetByIdAsync(id);
            return nationality != null ? _mapper.Map<NationalityDto>(nationality) : null;
        }

        public async Task<NationalityDto> GetNationalityByCountryCodeAsync(string countryCode)
        {
            var nationality = await _nationalityRepository.GetAllAsync(n => n.CountryCode == countryCode);
            return nationality != null ? _mapper.Map<NationalityDto>(nationality.FirstOrDefault()) : null;
        }

        public async Task<NationalityDto> CreateNationalityAsync(NationalityDto nationalityDto)
        {
            var nationality = _mapper.Map<Nationality>(nationalityDto);
            nationality.CreatedAt = DateTime.Now;

            await _nationalityRepository.AddAsync(nationality);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<NationalityDto>(nationality);
        }

        public async Task UpdateNationalityAsync(int id, NationalityDto nationalityDto)
        {
            var existingNationality = await _nationalityRepository.GetByIdAsync(id);
            if (existingNationality == null)
                throw new Exception($"Nationality with ID {id} not found");

            _mapper.Map(nationalityDto, existingNationality);
            existingNationality.ModifiedAt = DateTime.Now;
            existingNationality.ModifiedBy = "System"; // Replace with actual user

            _nationalityRepository.Update(existingNationality);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteNationalityAsync(int id)
        {
            var nationality = await _nationalityRepository.GetByIdAsync(id);
            if (nationality == null)
                throw new Exception($"Nationality with ID {id} not found");

            nationality.DeletedAt = DateTime.Now;
            nationality.DeletedBy = "System"; // Replace with actual user

            _nationalityRepository.Delete(nationality);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> NationalityExistsAsync(int id)
        {
            return await _nationalityRepository.ExistsAsync(n => n.Id == id);
        }

        public async Task<bool> CountryCodeExistsAsync(string countryCode)
        {
            return await _nationalityRepository.ExistsAsync(n => n.CountryCode == countryCode);
        }

        public async Task<List<UserDto>> GetUsersByNationalityAsync(int nationalityId)
        {
            var users = await _userRepository.GetAllAsync(u => u.NationalityId == nationalityId);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<int> GetUserCountByNationalityAsync(int nationalityId)
        {
            return await _userRepository.CountAsync(u => u.NationalityId == nationalityId);
        }
    }
}
