using AutoMapper;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IRepository<User> userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetUsersByRoleAsync(string role)
        {
            var users = await _userRepository.GetAllAsync(u => u.Role == role);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new Exception($"User with ID {id} not found");

            _mapper.Map(userDto, existingUser);
            _userRepository.Update(existingUser);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception($"User with ID {id} not found");

            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> UserExistsAsync(string id)
        {
            return await _userRepository.ExistsAsync(u => u.Id == id);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.FindAsync(u => u.Email == email);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<List<UserDto>> GetActiveUsersAsync()
        {
            var users = await _userRepository.GetAllAsync(u => u.IsActive);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _userRepository.CountAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null ? new List<string> { user.Role } : new List<string>();
        }

        public async Task AssignRoleAsync(int userId, string role)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.Role = role;
                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task RemoveRoleAsync(int userId, string role)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null && user.Role == role)
            {
                user.Role = "";
                _userRepository.Update(user);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
