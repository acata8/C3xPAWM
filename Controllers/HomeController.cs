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

namespace C3xPAWM.Controllers
{
    public class HomeController : Controller
    {
        private readonly INegoziService negoziService;

        public HomeController(INegoziService negoziService)
        {
            this.negoziService = negoziService;

        }

        public async Task<IActionResult> Index()
        {
            List<PubblicitaViewModel> negoziPubblicizzati = await negoziService.GetNegoziPubblicizzati();
            return View(negoziPubblicizzati);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
