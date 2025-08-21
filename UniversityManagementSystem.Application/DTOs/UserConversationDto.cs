namespace UniversityManagementSystem.Application.DTOs
{
    public class UserConversationDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }

        public int ConversationId { get; set; }
        public ConversationDto Conversation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
