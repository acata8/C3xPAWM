using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    [ResponseCache(CacheProfileName = "Elenco")]
    public class ElencoController : Controller
    {
        private readonly ICachedNegoziService negoziService;

        public ElencoController(ICachedNegoziService negoziService)
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

    }
}