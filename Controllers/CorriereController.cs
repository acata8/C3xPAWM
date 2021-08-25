using System;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Controllers
{
    [Authorize(Roles = nameof(Categoria.Corriere) + "," + nameof(Categoria.Administrator))]
    public class CorriereController : Controller
    {
        private readonly ICorriereService corriereService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<CorriereController> logger;
        public CorriereController(ICorriereService corriereService, SignInManager<ApplicationUser> signInManager, ILogger<CorriereController> logger)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.corriereService = corriereService;
        }


        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult Index(int id)
        {
            CorriereDashboardViewModel vm = new();
            vm.CorriereId = id;
            vm.Corriere = corriereService.GetCorriereID(id);
            vm.Pacchi = corriereService.GetCronologiaPacchi(id);
            vm.PacchiAssegnati = corriereService.GetNumeroPacchi(id);
            return View(vm);
        }


        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult NonAssegnati(int id)
        {

            PacchiListViewModel vm = new();
            vm.Corriere = corriereService.GetCorriereID(id);
            vm.CorriereId = id;
            vm.Pacchi = corriereService.GetPacchiNonAssegnati();
            return View(vm);
        }


        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpPost]
        public IActionResult NonAssegnati(PaccoViewModel model)
        {

            if (ModelState.IsValid)
            {
                var assegnato = corriereService.AssegnaPacco(model);
                if (assegnato)
                {
                    TempData["Success"] = "Pacco assegnato";
                    logger.LogInformation("Pacco assegnato");
                    
                }
                else
                {
                    TempData["Error"] = "Pacco non assegnato";
                    logger.LogInformation("Pacco non assegnato");
                }
                return RedirectToAction(nameof(Index), new { id = model.CorriereId });
            }
            TempData["Error"] = "Pacco non assegnato";
            logger.LogInformation("Informazioni inserite non valide,, Pacco non assegnato");
            return RedirectToAction(nameof(Index), new { id = model.CorriereId });
        }

        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpPost]
        public IActionResult Consegna(PaccoViewModel model)
        {
            model.Data = DateTime.Now;
            if (ModelState.IsValid)
            {
                var consegnato = corriereService.ConsegnaPacco(model);
                if (consegnato){
                    TempData["Success"] = "Pacco consegnato";
                    logger.LogInformation("Pacco consegnato");
                }
                else
                {
                    TempData["Error"] = "Pacco non consegnato";
                    logger.LogInformation("Pacco non consegnato");
                }
                return RedirectToAction(nameof(Index), new { id = model.CorriereId });
            }

            TempData["Error"] = "Pacco non consegnato";
            logger.LogInformation("Informazioni inserite non valide,, Pacco non consegnato");
            return RedirectToAction(nameof(Index), new { id = model.CorriereId });
        }

        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult Consegna(int id)
        {
            PacchiListViewModel vm = new();
            vm.Corriere = corriereService.GetCorriereID(id);
            vm.CorriereId = id;
            vm.Pacchi = corriereService.GetPacchiCorriere(id);
            return View(vm);
        }

        [Authorize(Policy = nameof(Policy.NuovaAttivita))]
        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new CorriereInputModel();
            return View(vm);
        }

        [Authorize(Policy = nameof(Policy.NuovaAttivita))]
        [HttpPost]
        public async Task<IActionResult> CreazioneAsync(CorriereInputModel model)
        {

            if (ModelState.IsValid)
            {
                await corriereService.CreateCorriereAsync(model);
                logger.LogInformation("Creazione Corriere riuscita!");
                return await Logout();
            }

            TempData["Error"] = "Creazione fallita";
            logger.LogInformation("Informazioni inserite non valide,, creazione corriere fallita!");
            return View(model);
        }


        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Success"] = "Salvataggio eseguito! Rieffettua il Login";
            logger.LogInformation("Utente non loggato. Salvataggio infomazioni effettuato.");
            return RedirectToAction("Index", new { controller = "Home" });
        }


        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult Modifica(int id)
        {
            CorriereInputModel inputModel = corriereService.GetCorriere(id);
            return View(inputModel);
        }

        [HttpPost]
        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        public IActionResult Modifica(CorriereInputModel model)
        {

            if (ModelState.IsValid)
            {
                var modificato = corriereService.EditCorriere(model);
                TempData["Success"] = "Salvataggio eseguito";
                
                return RedirectToAction(nameof(Index), new { id = model.CorriereId });
            }

            TempData["Error"] = "Modifica fallita";
            logger.LogInformation("Informazioni inserite non valide, errore nella modifica");
            return View(model);
        }

        [Authorize(Policy = nameof(Policy.CorriereAttivo))]
        [HttpGet]
        public IActionResult Cronologia(int id)
        {

            PacchiListViewModel vm = new();
            vm.Pacchi = corriereService.GetCronologiaPacchi(id);
            return View(vm);
        }
    }
}

