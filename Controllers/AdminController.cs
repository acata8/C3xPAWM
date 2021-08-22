using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C3xPAWM.Controllers
{

    [Authorize(Roles = nameof(Categoria.Administrator))]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminService adminService;
        private readonly C3PAWMDbContext dbContext;
        private readonly INegoziService negoziService;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminService adminService, C3PAWMDbContext dbContext, INegoziService negoziService)
        {
            this.negoziService = negoziService;
            this.dbContext = dbContext;
            this.adminService = adminService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> UsersAsync()
        {

            GestioneUserViewModel vm = new();
            vm.Utenti = await adminService.GetUtentiAsync("Utente");
            vm.Amministratori = await adminService.GetUtentiAsync("Administrator");
            return View(vm);
        }

        public IActionResult Gestione()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssegnaAsync(UserRoleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Inserimento non valido";
                return RedirectToAction(nameof(Gestione));
            }

            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Non corrisponde ad nessun utente";
                return RedirectToAction(nameof(Gestione));
            }

            IList<Claim> claims = await userManager.GetClaimsAsync(user);

            Claim roleClaim = new(ClaimTypes.Role, model.Ruolo.ToString());
            if (claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value))
            {
                TempData["Error"] = "Ruolo già assegnato all'utente";
                return RedirectToAction(nameof(Gestione));
            }

            IdentityResult result = await userManager.AddClaimAsync(user, roleClaim);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Operazione fallita";
                return RedirectToAction(nameof(Gestione));
            }

            if (model.Ruolo == Categoria.Commerciante)
            {
                AssegnaNegozio(user);
            }
            else if (model.Ruolo == Categoria.Corriere)
            {
                AssegnaCorriere(user);
            }

            TempData["Success"] = "Ruolo assegnato!";
            return RedirectToAction(nameof(Gestione));
        }


        [HttpPost]
        public async Task<IActionResult> Revoca(UserRoleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Inserimento non valido";
                return RedirectToAction(nameof(Gestione));
            }

            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Non corrisponde ad nessun utente";
                return RedirectToAction(nameof(Gestione));
            }

            IList<Claim> claims = await userManager.GetClaimsAsync(user);

            Claim roleClaim = new(ClaimTypes.Role, model.Ruolo.ToString());
            if (!claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value))
            {
                TempData["Error"] = "Ruolo non assegnato all'utente";
                return RedirectToAction(nameof(Gestione));
            }

            IdentityResult result = await userManager.RemoveClaimAsync(user, roleClaim);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Operazione fallita";
                return RedirectToAction(nameof(Gestione));
            }

            if (model.Ruolo == Categoria.Commerciante)
            {
                RevocaNegozio(user);
            }
            else if (model.Ruolo == Categoria.Corriere)
            {
                RevocaCorriere(user);
            }

            TempData["Success"] = "Ruolo revocato!";
            return RedirectToAction(nameof(Gestione));
        }

        #region Revoca e Assegna
        private void RevocaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            negozio.Revoca();
            dbContext.SaveChanges();
        }

        private void RevocaCorriere(ApplicationUser user)
        {
            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            corriere.Revoca();
            dbContext.SaveChanges();
        }

        private void AssegnaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            negozio.Assegna();
            dbContext.SaveChanges();
        }
        private void AssegnaCorriere(ApplicationUser user)
        {
            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            corriere.Assegna();
            dbContext.SaveChanges();
        }

        #endregion

        public async Task<IActionResult> NegoziAsync(ElencoListInputModel input)
        {
            ListViewModel<NegozioViewModel> negozi = await negoziService.GetNegozi(input, true);


            ElencoListViewModel viewModel = new ElencoListViewModel
            {
                Negozi = negozi,
                Input = input
            };

            return View(viewModel);
        }

    }
}