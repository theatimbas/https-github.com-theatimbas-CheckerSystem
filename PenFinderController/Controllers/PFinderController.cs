using Microsoft.AspNetCore.Mvc;

namespace PenFinderController.Controllers
{
    public class PFinderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
