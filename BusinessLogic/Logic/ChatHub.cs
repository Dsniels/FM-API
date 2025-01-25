using System;
using Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BusinessLogic.Logic;


public class ChatHub : Hub
{
    public async Task SendMessage(string name, string text)
    {
        var message = new ChatMessage
        {
            SenderName = name,
            Text = text,
            SentAt = DateTimeOffset.UtcNow
        };
        await Clients.All.SendAsync(
            "ReceiveMessage",
            message.SenderName, 
            message.SentAt, 
            message.Text
        );
    }

}

