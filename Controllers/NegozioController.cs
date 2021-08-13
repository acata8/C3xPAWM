using System.Threading.Tasks;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    
    [Authorize(Roles = (nameof(Categoria.Commerciante)+","+nameof(Categoria.Administrator)))]
    
    public class NegozioController : Controller
    {
        private readonly INegoziService negoziService;

        public NegozioController(INegoziService negoziService)
        {
            this.negoziService = negoziService;

        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Index(int id){
            //Lista di ordini del negozio
            NegozioDashboardViewModel vm = new();
            vm.NegozioId = id;
            vm.Negozio = negoziService.GetNegozio(id);
            return View(vm);
        }

        /*
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
        
        public async Task<IActionResult> emailTrovata(string email)
        {
            bool result = await negoziService.RicercaEmailAsync(email);
            return Json(result);
        }
        */

        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new NegozioCreateInputModel();
            return View(vm);
        }


        [HttpPost]
        public IActionResult Creazione(NegozioCreateInputModel model)
        {
            
            if(ModelState.IsValid){
                negoziService.CreateNegoziAsync(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Modifica(int id){
            NegozioEditInputModel inputModel = negoziService.GetNegozioEdit(id);
            return View(inputModel);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public IActionResult Modifica(NegozioEditInputModel model)
        {
            
            
            if(ModelState.IsValid){
                var modificato =  negoziService.EditNegozio(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index), new {id = model.NegozioId});
            }

            return View(model);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Pubblicita(int id){
            PubblicitaInputModel inputModel = negoziService.GetNegozioPubblicita(id);
            return View(inputModel);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
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