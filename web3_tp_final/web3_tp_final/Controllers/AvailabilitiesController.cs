using Microsoft.AspNetCore.Mvc;
using web3_tp_final.API;

namespace web3_tp_final.Controllers
{
    public class AvailabilitiesController : Controller
    {
        private static APIController _aPIController;

        public AvailabilitiesController (APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public IActionResult Index()
        {
            return View("Availabilities");
        }
    }
}
