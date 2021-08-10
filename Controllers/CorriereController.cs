using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class CorriereController : Controller
    {
        private readonly ICorriereService corriereService;
        public CorriereController(ICorriereService corriereService)
        {
            this.corriereService = corriereService;
        }

        public ActionResult Index(){
            //Ritornare la lista dei pacchi dove lui e corriere
            return View();
        }

        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new CorriereInputModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Creazione(CorriereInputModel model)
        {
            
            if(ModelState.IsValid){
                corriereService.CreateCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Modifica(int id){
            CorriereInputModel inputModel = corriereService.GetCorriere(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Modifica(CorriereInputModel model)
        {
            
            if(ModelState.IsValid){
                var modificato =  corriereService.EditCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Modifica), new {id = model.CorriereId});
            }

            return View(model);
        }

    }
}