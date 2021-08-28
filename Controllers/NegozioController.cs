using System;
using System.Threading;
using System.Threading.Tasks;
using C3xPAWM.Customizations.PdfExporter;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Controllers
{

    [Authorize(Roles = (nameof(Categoria.Commerciante) + "," + nameof(Categoria.Administrator)))]

    public class NegozioController : Controller
    {
        private readonly INegoziService negoziService;
        private readonly SignInManager<ApplicationUser> signInManager;


        private readonly ILogger<NegozioController> logger;

        public NegozioController(INegoziService negoziService, SignInManager<ApplicationUser> signInManager, ILogger<NegozioController> logger)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.negoziService = negoziService;

        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Index(int id)
        {
            //Lista di ordini del negozio
            NegozioDashboardViewModel vm = new();
            vm.NegozioId = id;
            vm.Negozio = negoziService.GetNegozio(id);

            
            return View(vm);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Cronologia(int id)
        {
            PacchiNegozioViewModel vm = new();
            vm.Pacchi = negoziService.GetPacchiNegozio(id);
            vm.NegozioId = id;
            vm.Negozio = negoziService.GetNegozio(id);

            return View(vm);

        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Pacco(int id)
        {
            PaccoCreateInputModel iM = new PaccoCreateInputModel();
            iM.NegozioId = id;
            iM.Negozio = negoziService.GetNegozio(id);
            iM.ViaP = iM.Negozio.Via;
            iM.RegioneP = iM.Negozio.Regione;
            iM.CittaP = iM.Negozio.Citta;
            iM.ProvinciaP = iM.Negozio.Provincia;
            return View(iM);
        }


        public IActionResult StampaPDF(int PaccoId){
            var pacco = negoziService.GetPacco(PaccoId);
            var result = negoziService.StampaPDF(pacco);
            if (result)
                {
                    TempData["Success"] = @"PDF Esportato in C:\C3";
                    logger.LogInformation("PDF esportato correttamente");
                }
                else
                {
                     TempData["Success"] = "PDF non esportato";
                    logger.LogInformation("PDF non esportato");
                }
           return RedirectToAction(nameof(Cronologia), new { id = pacco.NegozioId });
        }


        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public async Task<IActionResult> PaccoAsync(PaccoCreateInputModel model)
        {
            
            if (ModelState.IsValid)
            {
                model.Partenza = model.ViaP + ", " + model.CittaP + "(" + model.ProvinciaP + ")";
                model.Destinazione = model.ViaD + ", " + model.CittaD + "(" + model.ProvinciaD + ")";
                model.Utente = await negoziService.GetUtenteAsync(model.Email);
                model.UtenteId = model.Utente.Id;
                
                bool result = negoziService.CreateOrder(model);
                if (result)
                {
                    TempData["Success"] = "Salvataggio eseguito";
                    logger.LogInformation("Salvataggio eseguito con successo. Pacco Creato");
                }
                else
                {
                    TempData["Error"] = "Creazione fallita";
                    logger.LogInformation("Pacco non creato");
                }

                return RedirectToAction(nameof(Index), new { id = model.NegozioId });
            }

            logger.LogInformation("Informazioni inserite non valide, Pacco non creato");
            return View(model);
        }


        public async Task<IActionResult> emailTrovata(string email)
        {
            bool result = await negoziService.RicercaEmailAsync(email);
            return Json(result);
        }

        [Authorize(Policy = nameof(Policy.NuovaAttivita))]
        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new NegozioInputModel();
            return View(vm);
        }

        [Authorize(Policy = nameof(Policy.NuovaAttivita))]
        [HttpPost]
        public async Task<IActionResult> CreazioneAsync(NegozioInputModel model)
        {

            if (ModelState.IsValid)
            {
                await negoziService.CreateNegoziAsync(model);
                logger.LogInformation("Creazione negozio riuscita.");
                return await Logout();
            }

            TempData["Error"] = "Creazione fallita";
            logger.LogInformation("Informazioni inserite non valide, Pacco non creato");
            return View(model);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Success"] = "Salvataggio eseguito! Rieffettua il Login";
            logger.LogInformation("Utente non loggato. Salvataggio eseguito con successo!");
            return RedirectToAction("Index", new { controller = "Home" });

        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Modifica(int id)
        {
            NegozioInputModel inputModel = negoziService.GetNegozioEdit(id);
            return View(inputModel);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public IActionResult Modifica(NegozioInputModel model)
        {


            if (ModelState.IsValid)
            {
                var modificato = negoziService.EditNegozio(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index), new { id = model.NegozioId });
            }
            logger.LogInformation("Informazioni inserite non valide, Pacco non creato");
            return View(model);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Pubblicita(int id)
        {
            PubblicitaInputModel inputModel = negoziService.GetNegozioPubblicita(id);
            return View(inputModel);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public IActionResult Pubblicita(PubblicitaInputModel model)
        {

            if (ModelState.IsValid)
            {
                negoziService.CreatePubblicita(model);
                TempData["Success"] = "Salvataggio eseguito!";
                return RedirectToAction(nameof(Index), new { id = model.NegozioId });
            }
            
            logger.LogInformation("Informazioni inserite non valide, Pacco non creato");
            return View(model);
        }

       public IActionResult TipologiaNonEsistente(string tipologia)
        {
            bool result = false;

            Tipologia tipoInput;
            
            if(Enum.TryParse(tipologia, true, out tipoInput)){
               result = true;
            }
                
            return Json(result);

        }



    }
}