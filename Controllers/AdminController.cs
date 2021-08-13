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
        public AdminController(UserManager<ApplicationUser> userManager, IAdminService adminService)
        {
            this.adminService = adminService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> UsersAsync()
        {
            GestioneUserViewModel vm = new();
            vm.Utenti = await adminService.GetUtentiAsync("Utente");
            vm.Amministratori = await adminService.GetUtentiAsync("Administrator");
            vm.Commercianti =  await adminService.GetUtentiAsync("Commerciante");
            vm.Corrieri =  await adminService.GetUtentiAsync("Corriere");
            return View(vm);
        }

        public IActionResult Gestione(){
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
                TempData["Error"] = "Ruolo già assegnato all'utente";
                return RedirectToAction(nameof(Gestione));
            }

            IdentityResult result = await userManager.RemoveClaimAsync(user, roleClaim);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Operazione fallita";
                return RedirectToAction(nameof(Gestione));
            }

            TempData["Success"] = "Ruolo revocato!";
            return RedirectToAction(nameof(Gestione));
        }

    }
}