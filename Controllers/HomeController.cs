using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using C3xPAWM.Models;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Controllers
{
    public class HomeController : Controller
    {
        private readonly INegoziService negoziService;

        public HomeController(INegoziService negoziService)
        {
            this.negoziService = negoziService;

        }

       
        public IActionResult Index(ElencoListInputModel input)
        {
            ListViewModel<PubblicitaViewModel> negoziPubblicizzati =  negoziService.GetNegoziPubblicizzati(input);

            PubblicitaListViewModel viewModel = new PubblicitaListViewModel{
                NegoziPubblicizzati = negoziPubblicizzati,
                Input = input
                
            };

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
