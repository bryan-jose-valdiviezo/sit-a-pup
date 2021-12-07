using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Interface;

namespace web3_tp_final.Hubs
{
    public class NotificationUserHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;


        public NotificationUserHub(IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }

        public string GetConnectionId()
        {
            var httpContext = this.Context.GetHttpContext();

            var userId = httpContext.Request.Query["userId"];

            _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

            return Context.ConnectionId;
        }

        public async override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);
               
            }
            Debug.WriteLine("Query Received: "+ userId);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            _userConnectionManager.RemoveUserConnection(connectionId);
            var value = await Task.FromResult(0);
        }

        public async void SendNewMessage(string senderId,string receiverId, string message)
        {
            var connections = _userConnectionManager.GetUserConnections(receiverId.ToString());
            DateTime dateTime = DateTime.Now;
            Debug.WriteLine(dateTime.ToString());

            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {

                    await Clients.Client(connectionId).SendAsync("SendMessageToUser", senderId, message, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));

                }
            }
            //else
            //{
            //    Debug.WriteLine("Connection not found, sending to nobody");
            //}
        }

       

    }
}
