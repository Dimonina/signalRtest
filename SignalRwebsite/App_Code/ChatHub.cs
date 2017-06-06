using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRwebsite.App_Code
{
    public class ChatHub : Hub
    {
        private readonly Database _db = new Database();

        public void MessageReceived(Entry entry)
        {
            Clients.Group(entry.ChatId).messageReceived(entry.Name, entry.Message);
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
            MessageReceived(entry);
        }

        public void JoinChat(string chatId)
        {
            this.Groups.Add(Context.ConnectionId, chatId);
        }
    }
}