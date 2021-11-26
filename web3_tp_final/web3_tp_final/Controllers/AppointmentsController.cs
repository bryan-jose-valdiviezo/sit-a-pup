using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager,APIController api) : 
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            Appointment appointment = await _api.Get<Appointment>(id);

            return View(appointment);
        }
    }
}
