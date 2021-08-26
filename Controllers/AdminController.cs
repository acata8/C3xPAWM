using System;
using System.Collections;
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
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Controllers
{

    [Authorize(Roles = nameof(Categoria.Administrator))]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdminService adminService;
        private readonly C3PAWMDbContext dbContext;
        private readonly INegoziService negoziService;
        private readonly ICorriereService corriereService;
        private readonly ILogger<AdminController> logger;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminService adminService,
         C3PAWMDbContext dbContext, INegoziService negoziService, ICorriereService corriereService, ILogger<AdminController> logger)
        {
            this.logger = logger;
            this.corriereService = corriereService;
            this.negoziService = negoziService;
            this.dbContext = dbContext;
            this.adminService = adminService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> UsersAsync(ElencoListInputModel model)
        {
            ListViewModel<UtenteViewModel> utenti = await adminService.GetUtenteEmailAsync(model);


            UtenteListViewModel viewModel = new UtenteListViewModel
            {
                Utenti = utenti,
                Input = model
            };

            return View(viewModel);
        }

        #region GESTIONE


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
                logger.LogWarning("Informazioni inserite non valide,");
                return RedirectToAction(nameof(Gestione));
            }

            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Non corrisponde ad nessun utente";
                logger.LogWarning("User non trovato");
                return RedirectToAction(nameof(Gestione));
            }

            IList<Claim> claims = await userManager.GetClaimsAsync(user);

            Claim roleClaim = new(ClaimTypes.Role, model.Ruolo.ToString());
            if (claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value))
            {
                TempData["Error"] = "Ruolo già assegnato all'utente";
                logger.LogWarning("Ruolo gia' assegnato");
                return RedirectToAction(nameof(Gestione));
            }

            IdentityResult result = await userManager.AddClaimAsync(user, roleClaim);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Operazione fallita";
                logger.LogWarning("Assegnazione ruolo fallita");
                return RedirectToAction(nameof(Gestione));
            }

            if (model.Ruolo == Categoria.Commerciante)
            {
                AssegnaNegozio(user);
                logger.LogInformation("Negozio assegnato");
            }
            else if (model.Ruolo == Categoria.Corriere)
            {
                AssegnaCorriere(user);
                logger.LogInformation("Corriere assegnato");
            }
            user.Revocato = 0;
            await userManager.UpdateAsync(user);
            TempData["Success"] = "Ruolo assegnato!";
            logger.LogInformation($"Ruolo assegnato con successo a {user.Email}");
            return RedirectToAction(nameof(Gestione));
        }


        [HttpPost]
        public async Task<IActionResult> Revoca(UserRoleInputModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Inserimento non valido";
                logger.LogWarning("Informazioni inserite non valide,");
                return RedirectToAction(nameof(Gestione));
            }

            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "Non corrisponde ad nessun utente";
                logger.LogWarning("User non trovato");
                return RedirectToAction(nameof(Gestione));
            }

            IList<Claim> claims = await userManager.GetClaimsAsync(user);

            Claim roleClaim = new(ClaimTypes.Role, model.Ruolo.ToString());
            if (!claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value))
            {
                TempData["Error"] = "Ruolo non assegnato all'utente";
                logger.LogWarning("Ruolo non assegnato all'utente");
                return RedirectToAction(nameof(Gestione));
            }

            IdentityResult result = await userManager.RemoveClaimAsync(user, roleClaim);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Operazione fallita";
                logger.LogWarning("Assegnamento fallito");
                return RedirectToAction(nameof(Gestione));
            }

            if (model.Ruolo == Categoria.Commerciante)
            {
                RevocaNegozio(user);
                logger.LogInformation("Negozio revocato");
            }
            else if (model.Ruolo == Categoria.Corriere)
            {
                RevocaCorriere(user);
                logger.LogInformation("Corriere revocato");
            }

            await userManager.UpdateAsync(user);
            TempData["Success"] = "Ruolo revocato!";
            logger.LogInformation($"Ruolo revocato con successo a {user.Email}");
            return RedirectToAction(nameof(Gestione));
        }

        private void RevocaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            negozio.Revoca();
            user.Revocato = 1;
            dbContext.SaveChanges();
        }

        private void RevocaCorriere(ApplicationUser user)
        {
            try
            {
                dbContext.SaveChanges();
                var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
                corriere.Revoca();

                var pacchi = dbContext.Pacco.Where(c => c.CorriereId == corriere.CorriereId).ToList();
                foreach(var pacco in pacchi){
                    pacco.RevocaCorriere();
                }
                user.Revocato = 1;
                dbContext.SaveChanges();
                logger.LogInformation($"Revoca corriere riuscita.");
            }
            catch (Exception e)
            {
                logger.LogWarning($"Revoca corriere fallita. Eccezione: {e}");
                throw;
            }
            
            
        }

        private void AssegnaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            negozio.Assegna();
            user.Revocato = 0;
            dbContext.SaveChanges();
        }
        private void AssegnaCorriere(ApplicationUser user)
        {
            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            corriere.Assegna();
            user.Revocato = 0;
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


        public async Task<IActionResult> CorrieriAsync(ElencoListInputModel input)
        {
            ListViewModel<CorriereViewModel> corrieri = await corriereService.GetCorriere(input, true);


            CorriereListViewModel viewModel = new CorriereListViewModel
            {
                Corrieri = corrieri,
                Input = input
            };

            return View(viewModel);
        }

    }
}