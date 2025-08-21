
namespace UniversityManagementSystem.Domain.Entities
{
    public class Conversation : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsGroup { get; set; }

        // العلاقات
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserConversation> Participants { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
