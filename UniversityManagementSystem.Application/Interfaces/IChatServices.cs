using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IChatServices
    {
        Task<List<ConversationDto>> GetUserConversationsAsync(string userId);
        Task<ConversationDto> GetConversationByIdAsync(int id);
        Task<List<MessageDto>> GetConversationMessagesAsync(int conversationId);
        Task<MessageDto> SendMessageAsync(MessageDto messageDto);
        Task MarkAsReadAsync(int messageId);
        Task<ConversationDto> CreateConversationAsync(ConversationDto conversationDto);
        Task AddParticipantToConversationAsync(int conversationId, string userId);
    }
}
