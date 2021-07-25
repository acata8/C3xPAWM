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
        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListaNegozi(string search,
                                                     int page = 1)
        {
            List<NegozioViewModel> negozi = await negoziService.GetNegoziAsync(search, page);
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziCitta(string citta, int page = 1){
            List<NegozioViewModel> negozi = await negoziService.GetNegoziByCittaAsync(citta, page);
            return View(negozi);
        }

        public async Task<IActionResult> ListaNegoziProvincia(string provincia){
            List<NegozioViewModel> negozi = await negoziService.GetNegoziByProvinciaAsync(provincia);
            return View(negozi);
        }
    }
}