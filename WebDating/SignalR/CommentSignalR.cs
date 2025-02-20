using Microsoft.AspNetCore.SignalR;
using WebDating.DTOs.Post;

namespace WebDating.SignalR
{
    public class CommentSignalR : Hub
    {
        public async Task SendComment(CommentPostDto comment)
        {
            await Clients.All.SendAsync("ReceiveComment", comment);
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected: " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
