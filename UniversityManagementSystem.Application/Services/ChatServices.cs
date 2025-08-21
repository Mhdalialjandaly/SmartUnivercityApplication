using AutoMapper;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;

namespace UniversityManagementSystem.Application.Services
{
    public class ChatServices : Injectable, IChatServices
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<Conversation> _conversationRepository;
        private readonly IRepository<UserConversation> _userConversationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChatServices(
            IRepository<Message> messageRepository,
            IRepository<Conversation> conversationRepository,
            IRepository<UserConversation> userConversationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _userConversationRepository = userConversationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ConversationDto>> GetUserConversationsAsync(string userId)
        {
            var conversations = await _conversationRepository.GetAllAsync(c => c.Participants);

            return _mapper.Map<List<ConversationDto>>(conversations);
        }

        public async Task<ConversationDto> GetConversationByIdAsync(int id)
        {
            var conversation = await _conversationRepository.GetAllAsync(
                c => c.Id == id,c => c.Participants);

            return _mapper.Map<ConversationDto>(conversation);
        }

        public async Task<List<MessageDto>> GetConversationMessagesAsync(int conversationId)
        {
            var messages = await _messageRepository.GetAllAsync(
                m => m.ConversationId == conversationId);

            return _mapper.Map<List<MessageDto>>(messages);
        }

        public async Task<MessageDto> SendMessageAsync(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            message.SentAt = DateTime.Now;
            message.IsRead = false;

            await _messageRepository.AddAsync(message);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<MessageDto>(message);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                _messageRepository.Update(message);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<ConversationDto> CreateConversationAsync(ConversationDto conversationDto)
        {
            var conversation = _mapper.Map<Conversation>(conversationDto);
            conversation.CreatedAt = DateTime.Now;

            await _conversationRepository.AddAsync(conversation);
            await _unitOfWork.CommitAsync();

            // إضافة المشاركين
            foreach (var participant in conversationDto.Participants)
            {
                await AddParticipantToConversationAsync(conversation.Id, participant.UserId);
            }

            return _mapper.Map<ConversationDto>(conversation);
        }

        public async Task AddParticipantToConversationAsync(int conversationId, string userId)
        {
            var userConversation = new UserConversation
            {
                UserId = userId,
                ConversationId = conversationId
            };

            await _userConversationRepository.AddAsync(userConversation);
            await _unitOfWork.CommitAsync();
        }
    }
}
