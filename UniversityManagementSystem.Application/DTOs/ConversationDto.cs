
namespace UniversityManagementSystem.Application.DTOs
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsGroup { get; set; }
        public int UnreadCount { get; set; }
        // العلاقات
        public List<MessageDto> Messages { get; set; }
        public List<UserConversationDto> Participants { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
