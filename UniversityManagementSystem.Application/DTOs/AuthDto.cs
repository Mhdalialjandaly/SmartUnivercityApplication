namespace UniversityManagementSystem.Application.DTOs
{
    public class AuthDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
