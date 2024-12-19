using Microsoft.AspNetCore.SignalR;

namespace WebMVC.ChatHub
{
    public class ChatHub: Hub
    {
        // Hàm để client tham gia vào room
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        // Hàm để client rời khỏi room
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        // Khi client kết nối
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // UserId từ Token hoặc Authentication Middleware
            if (!string.IsNullOrEmpty(userId))
            {
                // Tự động thêm user vào room riêng theo userId
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, "Common");
            await base.OnConnectedAsync();
        }

        // Khi client ngắt kết nối
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                // Xử lý logic nếu cần, chẳng hạn rời khỏi các room
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // Hàm để gửi thông báo đến một room cụ thể
        public async Task SendMessageToRoom(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", message);
        }

    }
}
