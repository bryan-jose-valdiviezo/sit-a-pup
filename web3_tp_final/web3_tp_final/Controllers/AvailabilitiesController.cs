using Microsoft.AspNetCore.Mvc;

namespace web3_tp_final.Controllers
{
    public class AvailabilitiesController : Controller
    {
        public IActionResult Index()
        {
            return View("Availabilities");
        }
    }
}
