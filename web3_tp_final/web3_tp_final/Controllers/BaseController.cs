using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class BaseController : Controller
    {
        private APIController _api = new APIController();

        public User CurrentUser()
        {
            return SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
        }

        public APIController Api()
        {
            return _api;
        }
    }
}
