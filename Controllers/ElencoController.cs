using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        public async Task<IActionResult> Index()
        {
            List<string> citta = await negoziService.GetListaCittaDistinct();
            List<string> regioni = await negoziService.GetListaRegioniDistinct();
            var tuple = new Tuple<List<string>, List<string>>(citta,regioni);
            return View(tuple);
        }
        public async Task<IActionResult> ListaNegozi(string search,
                                                     int page = 1)
        {
            List<NegozioViewModel> negozi = await negoziService.GetNegoziAsync(search, page);
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziCitta(string x){
            List<NegozioViewModel> negozi = await negoziService.GetNegoziByCittaAsync(x);
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziRegione(string regione){
            List<NegozioViewModel> negozi = await negoziService.GetNegoziByRegioneAsync(regione);
            return View(negozi);
        }
    }
}