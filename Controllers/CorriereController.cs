using System.Threading.Tasks;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{
    [Authorize(Roles = nameof(Categoria.Corriere)+","+nameof(Categoria.Administrator))]
    public class CorriereController : Controller
    {
        private readonly ICorriereService corriereService;
        public CorriereController(ICorriereService corriereService)
        {
            this.corriereService = corriereService;
        }

       
        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult Index(int id)
        {
            /*
            ListViewModel<PaccoViewModel> pacchiCorriere = corriereService.GetPacchiNonAssegnati();

            PacchiListViewModel viewModel = new PacchiListViewModel{
                PacchiNonAssegnati = pacchiNonAssegnati
            };

            return View(viewModel);
            */
            CorriereDashboardViewModel vm = new();
            vm.CorriereId = id;
            vm.Corriere = corriereService.GetCorriereID(id);
            vm.Pacchi = corriereService.GetPacchiCorriere(id);
            return View(vm);

            
        }

        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult NonAssegnati(int id){
           
            PacchiListViewModel vm = new();
            vm.Corriere = corriereService.GetCorriereID(id);
            vm.CorriereId = id;
            vm.Pacchi = corriereService.GetPacchiNonAssegnati();
            return View(vm);
        }

        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpPost]
        public IActionResult NonAssegnati(PaccoViewModel model){
                        
            if(ModelState.IsValid){
                var assegnato = corriereService.AssegnaPacco(model);
                if(assegnato)
                    TempData["Success"] = "Pacco assegnato";
                else
                {
                    TempData["Error"] = "Pacco non assegnato";
                }
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
            
            
        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new CorriereInputModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Creazione(CorriereInputModel model)
        {
            
            if(ModelState.IsValid){
                corriereService.CreateCorriereAsync(model);
                TempData["Success"] = "Salvataggio eseguito";
                return LocalRedirect("/Elenco");
            }

            TempData["Error"] = "Creazione fallita";
            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        public IActionResult Modifica(int id){
            CorriereInputModel inputModel = corriereService.GetCorriere(id);
            return View(inputModel);
        }

        [HttpPost]
        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        public IActionResult Modifica(CorriereInputModel model)
        {
            
            if(ModelState.IsValid){
                var modificato =  corriereService.EditCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index), new {id = model.CorriereId});
            }

            TempData["Error"] = "Modifica fallita";
            return View(model);
        }

        
    }   
}

