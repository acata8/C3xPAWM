using C3xPAWM.Models.InputModel;
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

        public IActionResult Index()
        {
            //Lista degli ordini dell'utente loggato
            return View();
        }
         [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new UtenteInputModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Creazione(UtenteInputModel model)
        {
            
            if(ModelState.IsValid){
                utenteService.CreateUtente(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Modifica(int id){
            UtenteInputModel inputModel = utenteService.GetUtente(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Modifica(UtenteInputModel model)
        {
            
            if(ModelState.IsValid){
                var modificato =  utenteService.EditUtente(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Modifica), new {id = model.UtenteId});
            }

            return View(model);
        }

    }
}