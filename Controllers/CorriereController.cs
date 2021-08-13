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

       
        
        [HttpGet]
        public IActionResult Index()
        {

            /*
            ListViewModel<PaccoViewModel> pacchiCorriere = corriereService.GetPacchiNonAssegnati();

            PacchiListViewModel viewModel = new PacchiListViewModel{
                PacchiNonAssegnati = pacchiNonAssegnati
            };

            return View(viewModel);
            */
            return View();
        }

        /*
        [HttpGet]
        public IActionResult NonAssegnati(int id){
            ListViewModel<PaccoViewModel> pacchiNonAssegnati = corriereService.GetPacchiNonAssegnati();
            var corriere = id;
            PacchiListViewModel viewModel = new PacchiListViewModel{
                PacchiNonAssegnati = pacchiNonAssegnati,
                CorriereId = id
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NonAssegnati(PaccoAssegnatoViewModel model){
                        
            if(ModelState.IsValid){
                var assegnato = corriereService.AssegnaPacco(model);
                if(assegnato)
                    TempData["Success"] = "Pacco assegnato";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
            */
            
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
                corriereService.CreateCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Modifica(int id){
            CorriereInputModel inputModel = corriereService.GetCorriere(id);
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Modifica(CorriereInputModel model)
        {
            
            if(ModelState.IsValid){
                var modificato =  corriereService.EditCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Modifica), new {id = model.CorriereId});
            }

            return View(model);
        }

        
    }   
}

