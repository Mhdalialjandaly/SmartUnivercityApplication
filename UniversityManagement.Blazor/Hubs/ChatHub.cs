using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace UniversityManagement.Blazor.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinConversation(int conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
        }

        public async Task SendMessage(int conversationId, string content, int senderId)
        {
            // يمكنك حفظ الرسالة في قاعدة البيانات هنا أولاً
            await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", new
            {
                Content = content,
                SenderId = senderId,
                SentAt = DateTime.Now
            });
        }
    }
}
