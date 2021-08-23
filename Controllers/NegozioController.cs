using System.Threading;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{

    [Authorize(Roles = (nameof(Categoria.Commerciante) + "," + nameof(Categoria.Administrator)))]

    public class NegozioController : Controller
    {
        private readonly INegoziService negoziService;
        private readonly SignInManager<ApplicationUser> signInManager;

        

        public NegozioController(INegoziService negoziService, SignInManager<ApplicationUser> signInManager)
        {
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
            vm.pacchi = negoziService.GetPacchiNegozio(id);
            return View(vm);
        }


        [HttpGet]
        public IActionResult Pacco(int id)
        {
            PaccoCreateInputModel inputModel = new PaccoCreateInputModel();
            inputModel.NegozioId = id;
            inputModel.Partenza = negoziService.getIndirizzo(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> PaccoAsync(PaccoCreateInputModel model)
        {
            try
            {
                model.Utente = await negoziService.GetUtenteAsync(model.Email);
                model.UtenteId = model.Utente.Id;
            }
            catch (System.Exception)
            {

                throw;
            }

            if (ModelState.IsValid)
            {
                bool result = negoziService.CreateOrder(model);
                if (result)
                {

                    TempData["Success"] = "Salvataggio eseguito";
                }
                else
                {
                    TempData["Error"] = "Creazione fallita";
                }

                return RedirectToAction(nameof(Index), new { id = model.NegozioId });
            }

            return View(model);
        }


        public async Task<IActionResult> emailTrovata(string email)
        {
            bool result = await negoziService.RicercaEmailAsync(email);
            return Json(result);
        }


        [HttpGet]
        public IActionResult Creazione()
        {
            var vm = new NegozioCreateInputModel();
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> CreazioneAsync(NegozioCreateInputModel model)
        {

            if (ModelState.IsValid)
            {
                await negoziService.CreateNegoziAsync(model);
                return await Logout();
            }

            TempData["Error"] = "Creazione fallita";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Success"] = "Salvataggio eseguito! Rieffettua il Login";
            return RedirectToAction("Index", new { controller = "Home" });
            
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpGet]
        public IActionResult Modifica(int id)
        {
            NegozioEditInputModel inputModel = negoziService.GetNegozioEdit(id);
            return View(inputModel);
        }

        [Authorize(Policy = nameof(Policy.ProprietarioNegozio))]
        [HttpPost]
        public IActionResult Modifica(NegozioEditInputModel model)
        {


            if (ModelState.IsValid)
            {
                var modificato = negoziService.EditNegozio(model);
                TempData["Success"] = "Salvataggio eseguito";
                return RedirectToAction(nameof(Index), new { id = model.NegozioId });
            }

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

            return View(model);
        }



    }
}