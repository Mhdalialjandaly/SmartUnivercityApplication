using Microsoft.AspNetCore.Identity;

namespace UniversityManagementSystem.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName}" + "-" + $"{LastName}"; 

        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string PhotoUrl { get; set; }

        public int? NationalityId { get; set; } 

        public string Avatar { get; set; }
        public Nationality Nationality { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}