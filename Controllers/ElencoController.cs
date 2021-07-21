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
        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ListaNegozi(){
            List<NegozioViewModel> negozi = await negoziService.getNegozi();
            return View(negozi);
        }

       

    }
}