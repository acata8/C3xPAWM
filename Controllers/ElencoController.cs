using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class ElencoController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Regione(string regione){
            return View();
        }

        public IActionResult Provincia(string provincia){
            return View();
        }

        public IActionResult Citta(string citta){
            return View();
        }

    }
}