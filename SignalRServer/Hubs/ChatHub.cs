using Microsoft.AspNetCore.SignalR;
using SignalRServer.Models;
using System.Text.Json;

namespace SignalRServer.Hubs;

public class ChatHub : Hub
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("--> Connection Established " + Context.ConnectionId);
        Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnId", Context.ConnectionId);
        return base.OnConnectedAsync();
    } 

    public async Task SendMessageAsync(string messageInput)
    {
        Message message = JsonSerializer.Deserialize<Message>(messageInput)!;

        string Recipient = message.To;

        Console.WriteLine("Message Recieved on: " + Context.ConnectionId);

        if(!String.IsNullOrEmpty(message.To))
        {
            await Clients.Client(message.To).SendAsync("RecieveMessage", message.Body);
        }
        else
        {
            await Clients.All.SendAsync("RecieveMessage", message.Body);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
