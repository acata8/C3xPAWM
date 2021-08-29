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
        private readonly INegoziService negoziService;
        private readonly ICorriereService corriereService;
        private readonly ILogger<AdminController> logger;

        public AdminController(UserManager<ApplicationUser> userManager, IAdminService adminService, INegoziService negoziService, ICorriereService corriereService, ILogger<AdminController> logger)
        {
            this.logger = logger;
            this.corriereService = corriereService;
            this.negoziService = negoziService;
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

        [HttpGet]
        public IActionResult Gestione()
        {
            return View();
        }

        public async Task<IActionResult> GestioneRuoloAsync(string btnRuolo, UserRoleInputModel model){

            if (!ModelState.IsValid){
                TempData["Error"] = "Inserimento non valido";
                logger.LogWarning("Informazioni inserite non valide,");
            }
            else{   
                var notifica = 0;
                var ruoloDaAssegnare = model.Ruolo.ToString();
                ApplicationUser user = await userManager.FindByEmailAsync(model.Email);
                var claims = await userManager.GetClaimsAsync(user);
                Claim roleClaim = new(ClaimTypes.Role, ruoloDaAssegnare);
                if (btnRuolo.Equals("Assegna"))
                {
                    notifica = await adminService.AssegnaAsync(user, claims, ruoloDaAssegnare, roleClaim);
                }
                else if (btnRuolo.Equals("Revoca"))
                {
                    notifica = await adminService.RevocaAsync(user, claims, ruoloDaAssegnare, roleClaim);
                }


                string messaggio = "Error";
                if(notifica > 5){
                    messaggio = "Success";
                    await userManager.UpdateAsync(user);

                    if(notifica == 7){

                        logger.LogInformation($"Ruolo assegnato con successo a {user.Email}");
                    }
                    if(notifica == 6){
                        logger.LogInformation($"Ruolo revocato con successo a {user.Email}");
                    }
                }
                    

                TempData[messaggio] = (notifica) switch
                {
                    (1) => "Non corrisponde ad nessun utente",
                    (2) => $"Ruolo attivo dell'utente: {claims[1].Value.ToString()}, revocalo per assegnare {ruoloDaAssegnare}",
                    (3) => "Ruolo già assegnato all'utente",
                    (4) => "Operazione fallita",
                    (5) =>  "Ruolo non assegnato all'utente",
                    (6) =>  "Ruolo revocato",
                    _ =>  "Ruolo assegnato con successo"
                };
            }

            return RedirectToAction(nameof(Gestione));
        }

       
    
        [HttpPost]
        public async Task<IActionResult> Token(string btnToken, UserRoleInputModel model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.Email);  
            var negozio = negoziService.GetNegozio(user.IdRuolo);
            bool success = false;
            if(negozio.ProprietarioId == user.Id){
                var token = model.Token;
                if(btnToken.Equals("Aggiunta")){
                    success = adminService.AggiungiToken(negozio, token);
                }else if(btnToken.Equals("Rimozione")){
                    success = adminService.RimuoviToken(negozio, token);
                }
            }
           
            if(success){
                TempData["Success"] = "Operazione riuscita";
            }else
                 TempData["Error"] = "Operazione fallita";

             return RedirectToAction(nameof(Gestione));
        }

        [HttpGet]
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

        [HttpGet]
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