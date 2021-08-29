
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using C3xPAWM.Models;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using C3xPAWM.Models.InputModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Controllers
{
    public class HomeController : Controller
    {

        private readonly INegoziService negoziService;

        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(INegoziService negoziService, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.negoziService = negoziService;
        }

        [AllowAnonymous]
        public IActionResult Index(ElencoListInputModel input)
        {
            ListViewModel<PubblicitaViewModel> negoziPubblicizzati = negoziService.GetNegoziPubblicizzati(input);

            PubblicitaListViewModel viewModel = new PubblicitaListViewModel
            {
                NegoziPubblicizzati = negoziPubblicizzati,
                Input = input
            };
            
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Success"] = "Logout riuscito";
            return RedirectToAction(nameof(Index));
        }
        
    }
}
