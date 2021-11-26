using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserConnectionManager _userConnectionManager;

        public ChatHub(IUserConnectionManager userConnectionManager)
        {
            _userConnectionManager = userConnectionManager;
        }
        public Task SendPrivateMessage(string user, string message)
        {
            List<string> userIdentifier = _userConnectionManager.GetUserIdentifiers(user);
            
            return Clients.User(userIdentifier.First()).SendAsync("ReceiveMessage", message);
        }
      /*  public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

         public async Task SendMessageToGroup(string sender, string receiver, string message)
         {
            await Clients.User(receiver).SendAsync("ReceiveMessage", sender, message);
         }*/
    }
}
