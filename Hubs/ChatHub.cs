
using System.Threading.Tasks;
using System;
using JobsityChallengeAPI.Services;
using Microsoft.AspNetCore.SignalR;
using JobsityChallengeAPI.DTOs;

namespace JobsityChallengeAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatRoom _chatRoom;
        private readonly Stock _stock;
        public ChatHub(ChatRoom chatRoom, Stock stock)
        {
            _chatRoom = chatRoom;
            _stock = stock;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatApi");
            await Clients.Caller.SendAsync("UserConnected");

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ChatApi");
            var user = _chatRoom.GetUserByConnectionId(Context.ConnectionId);
            if(user != null)
            {
                _chatRoom.RemoveUserFromList(user);
                await DisplayOnlineUsers();
            }
            
            await base.OnDisconnectedAsync(exception);
        }
        public async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatRoom.GetOnlineUsers();

            await Clients.Others.SendAsync("OnlineUsers", onlineUsers);
            await Clients.Caller.SendAsync("OnlineUsers", onlineUsers);
        }
        public async Task AddUserConnectionId(string name)
        {
            _chatRoom.AddUserConnectionId(name, Context.ConnectionId);
            await DisplayOnlineUsers();
        }
        public async Task ReceiveMessage(MessageDTO message)
        {
            if (message.Content.Contains("/stock="))
            {
                var stock = message.Content.Split('=');
               
                var  stock_val = await _stock.getStock(stock[1]);
                var res =stock_val.Split("\n");
                res = res[1].Split(",");
                var msgStr = res[0] + " quote is " + res[6] + " per share";
                MessageDTO msg = new()
                {
                    Content = msgStr,
                    From = "Stock Bot",
                    TimeStamp = message.TimeStamp
                };             

                await Clients.Caller.SendAsync("NewMessage", msg);

            }
            else
            {
                await Clients.Group("ChatApi").SendAsync("NewMessage", message);
            }
            
        }

    }
}
