using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class ElencoController : Controller
    {
        
        private readonly INegoziService negoziService;
        public ElencoController(INegoziService negoziService){
            this.negoziService = negoziService;
        }
        
        public async Task<IActionResult> Index()
        {
            List<string> citta = await negoziService.getListaCittaDistinct();
            List<string> regioni = await negoziService.getListaRegioniDistinct();
            var tuple = new Tuple<List<string>, List<string>>(citta,regioni);
            return View(tuple);
        }
        public async Task<IActionResult> ListaNegozi(){
            List<NegozioViewModel> negozi = await negoziService.getNegoziAsync();
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziCitta(string x){
            List<NegozioViewModel> negozi = await negoziService.getNegoziByCittaAsync(x);
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziRegione(string x){
            List<NegozioViewModel> negozi = await negoziService.getNegoziByRegioneAsync(x);
            return View(negozi);
        }
    }
}