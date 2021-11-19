using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;

namespace web3_tp_final.Controllers
{
    public class ChatController : Controller
    {
        private static APIController _aPIController;

        public ChatController(APIController aPIController)
        {
            _aPIController = aPIController;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
