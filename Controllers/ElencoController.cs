using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    [ResponseCache(CacheProfileName = "Elenco")]
    public class ElencoController : Controller
    {
        private readonly INegoziService negoziService;

        public ElencoController(INegoziService negoziService)
        {
            this.negoziService = negoziService;
        }

        public async Task<IActionResult> Index(ElencoListInputModel input)
        {
            ListViewModel<NegozioViewModel> negozi = await negoziService.GetNegozi(input);
            

            ElencoListViewModel viewModel = new ElencoListViewModel
            {
                Negozi = negozi,
                Input = input
            };

            return View(viewModel);
        }

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
    }
   
}
