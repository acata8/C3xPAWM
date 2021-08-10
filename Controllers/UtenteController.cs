using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class UtenteController : Controller
    {
        private readonly IUtenteService utenteService;

        public UtenteController(IUtenteService utenteService)
        {
            this.utenteService = utenteService;
        }

        public ActionResult Index()
        {
            //Lista degli ordini dell'utente loggato
            return View();
        }

    }
}