namespace UniversityManagementSystem.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Role { get; set; } 
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string Department { get; set; } 
        public string Position { get; set; } 
    }
}
