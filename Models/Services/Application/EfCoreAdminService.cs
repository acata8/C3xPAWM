using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Options;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreAdminService : IAdminService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<EfCoreAdminService> logger;

        public EfCoreAdminService(C3PAWMDbContext dbContext, IOptionsMonitor<ElencoOptions> elencoOptions, UserManager<ApplicationUser> userManager, ILogger<EfCoreAdminService> logger)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.elencoOptions = elencoOptions;
            this.dbContext = dbContext;
        }

        public async Task<ListViewModel<UtenteViewModel>> GetUtenteEmailAsync(ElencoListInputModel model)
        {
            IQueryable<ApplicationUser> baseQuery = dbContext.Users;

            IQueryable<UtenteViewModel> queryLinq = baseQuery
                   .AsNoTracking()
                   .Select(u => new UtenteViewModel
                   {
                       Email = u.Email,
                       Ruolo = u.Ruolo,
                       Nome = u.FullName,
                       Proprietario = u.Proprietario,
                       Revocato = u.Revocato
                   }).OrderBy(c => c.Ruolo);

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                queryLinq = queryLinq.Where(u => u.Email.ToLower().Equals(model.Search.ToLower()));
            }

            var totale = queryLinq.Count();

            model.Paginare = true;

            List<UtenteViewModel> utenti = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
            .OrderBy(u => u.Nome)
            .ToListAsync();

            ListViewModel<UtenteViewModel> listViewModel = new ListViewModel<UtenteViewModel>
            {
                Elenco = utenti,
                TotaleElenco = totale
            };

            return listViewModel;
        }

        public async Task<IList<ApplicationUser>> GetUtentiAsync(string ruolo)
        {
            Claim claim = new(ClaimTypes.Role, ruolo);

            IList<ApplicationUser> userInRole = await userManager.GetUsersForClaimAsync(claim);

            return userInRole;
        }

        public void RevocaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            
            try
            {
                negozio.Revoca();
                user.Revocato = 1;
                dbContext.SaveChanges();
                logger.LogInformation($"Revoca negozio riuscita.");
            }
            catch (System.Exception)
            {
                negozio.Assegna();
                user.Revocato = 0;
                logger.LogWarning($"Revoca negozio fallita.");
                throw;
            }
            
        }

        public void RevocaCorriere(ApplicationUser user)
        {

            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            corriere.Revoca();
            var pacchi = dbContext.Pacco.Where(c => c.CorriereId == corriere.CorriereId).ToList();
            foreach (var pacco in pacchi)
            {
                pacco.RevocaCorriere();
            }
            user.Revocato = 1;
            try
            {
                dbContext.SaveChanges();
                logger.LogInformation($"Revoca corriere riuscita.");
            }
            catch (Exception e)
            {
                corriere.Assegna();
                foreach (var pacco in pacchi)
                {
                    pacco.SettaCorriere(corriere.CorriereId);

                }
                logger.LogWarning($"Revoca corriere fallita. Eccezione: {e}");
                throw;
            }


        }
        public void AssegnaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            negozio.Assegna();
            user.Revocato = 0;
            try
            {
                
                dbContext.SaveChanges();
                logger.LogInformation($"Riassegnamento negozio riuscito.");
            }
            catch (Exception)
            {
                negozio.Revoca();
                user.Revocato = 1;
                logger.LogInformation($"Riassegnamento negozio fallito.");
                throw;
            }
            
        }
        public void AssegnaCorriere(ApplicationUser user)
        {
            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            corriere.Assegna();
            user.Revocato = 0;
            try
            {
                dbContext.SaveChanges();
                logger.LogInformation($"Riassegnamento corriere riuscito.");
            }
            catch (System.Exception)
            {
                corriere.Revoca();
                user.Revocato = 1;
                logger.LogInformation($"Riassegnamento corriere fallito.");
                throw;
            }
        }

    }
}