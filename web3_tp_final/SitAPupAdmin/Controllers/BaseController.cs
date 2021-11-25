using Microsoft.AspNetCore.Mvc;
using SitAPupAdmin.API;
using SitAPupAdmin.Helpers;
using SitAPupAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Controllers
{
    public class BaseController : Controller
    {
        private APIController _api = new APIController();

        public Admin CurrentUser()
        {
            return SessionHelper.GetObjectFromJson<Admin>(HttpContext.Session, "admin");
        }

        public APIController Api()
        {
            return _api;
        }
    }
}
