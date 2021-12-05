using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class BaseController : Controller
    {
        protected APIController _api;
        protected readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        protected readonly IUserConnectionManager _userConnectionManager;

        public BaseController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api)
        {
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _api = api;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CurrentUser = GetCurrentUser();
            base.OnActionExecuting(filterContext);
        }

        public User GetCurrentUser()
        {
            return SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
        }

        public async void SendNewAppointmentNotification(int sitterId, int appointmentId)
        {
            var connections = _userConnectionManager.GetUserConnections(sitterId.ToString());
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendNewFormToUser", appointmentId);
                }
            }
        }
    }
}
