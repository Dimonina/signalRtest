using System;
using System.Linq;
using Microsoft.AspNet.SignalR;

namespace SignalRwebsite.App_Code
{
    public class ChatHub : Hub
    {
        private readonly Database _db = new Database();

        public void GetChat(string chatId)
        {
            var q = _db.Entries.AsQueryable();
            if (chatId == "-100")
            {
                q = q.Where(x => x.IsApproved == false);
            }
            else
            {
                q = q.Where(x => x.ChatId == chatId && x.IsApproved);
            }
            var clientMessages = _db.Entries.Where(x => x.ChatId == chatId && x.IsApproved).ToArray();
            var approverMessages = _db.Entries.Where(x => x.IsApproved == false).ToArray();

            Clients.Group(chatId).getChat(clientMessages);
            Clients.Group("-100").getChat(approverMessages);
        }

        public void SendMessage(string chatId, string name, string message)
        {
            Entry entry = new Entry()
            {
                ChatId = chatId,
                Name = name,
                Message = message,
                MessageId = Guid.NewGuid()
            };
            _db.Entries.Add(entry);
            GetChat(chatId);
        }

        public void JoinChat(string chatId)
        {
            this.Groups.Add(Context.ConnectionId, chatId);
        }

        public void ApproveMessage(Entry message)
        {
            _db.Entries.Where(x => x.MessageId == message.MessageId).ToList().ForEach(x => x.IsApproved = true);
            GetChat(message.ChatId);

        }
    }
}