using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    public class UtenteController : Controller
    {
        private readonly IUtenteService utenteService;
        public UtenteController(IUtenteService utenteService)
        {
            this.utenteService = utenteService;

        }

        public async Task<IActionResult> OrdiniAsync(ElencoListInputModel model)
        {
            ListViewModel<PaccoViewModel> pacchi = await utenteService.GetOrdini(model);
            
            OrdiniUtenteViewModel vm = new OrdiniUtenteViewModel{
                Ordini = pacchi,
                Input = model
            };

            return View(vm);

        }

    }
}