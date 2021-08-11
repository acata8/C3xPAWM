using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class NegozioController : Controller
    {
        private readonly INegoziService negoziService;

        public NegozioController(INegoziService negoziService)
        {
            this.negoziService = negoziService;

        }

        [HttpGet]
        public IActionResult Index(){
            //Lista di ordini del negozio
            return View();
        }

         [HttpGet]
        public IActionResult Pacco(int id){
            PaccoInputModel inputModel = negoziService.GetNegozioPacco(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Pacco(PaccoInputModel model){

            if(ModelState.IsValid){
                negoziService.CreateOrder(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new NegozioCreateInputModel();
            return View(vm);
        }

        public async Task<IActionResult> emailTrovata(string email)
        {
            bool result = await negoziService.RicercaEmailAsync(email);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Creazione(NegozioCreateInputModel model)
        {
            
            if(ModelState.IsValid){
                negoziService.CreateNegozi(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Modifica(int id){
            NegozioEditInputModel inputModel = negoziService.GetNegozio(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Modifica(NegozioEditInputModel model)
        {
            
            
            if(ModelState.IsValid){
                var modificato =  negoziService.EditNegozio(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Modifica), new {id = model.NegozioId});
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Pubblicita(int id){
            PubblicitaInputModel inputModel = negoziService.GetNegozioPubblicita(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Pubblicita(PubblicitaInputModel model)
        {
            
            if(ModelState.IsValid){
                negoziService.CreatePubblicita(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index), new {id = model.NegozioId});
            }

            return View(model);
        }

    }
}