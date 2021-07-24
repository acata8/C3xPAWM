using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}