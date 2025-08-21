namespace UniversityManagementSystem.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; } 
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }
        public int? NationalityId  { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string Role  { get; set; }
        public string PasswordHash  { get; set; }
        public string PhotoUrl   { get; set; }
        public bool IsActive { get; set; }
        public string Department { get; set; } 
        public string Position { get; set; }
        public string Avatar { get; set; }
        public string LockoutEnd  { get; set; }
        public string SecurityStamp   { get; set; }
        public bool LockoutEnabled  { get; set; }
        public bool TwoFactorEnabled   { get; set; }
        public bool PhoneNumberConfirmed   { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
