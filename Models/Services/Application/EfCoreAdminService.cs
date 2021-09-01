using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
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
                       Revocato = u.Revocato,
                       Utente = u
                   });

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                queryLinq = queryLinq.Where(u => u.Email.ToLower().Equals(model.Search.ToLower()));
            }

            var totale = queryLinq.Count();

            model.Paginare = true;

            List<UtenteViewModel> utenti = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
            .OrderBy(u => u.Ruolo)
            .ToListAsync();

            ListViewModel<UtenteViewModel> listViewModel = new ListViewModel<UtenteViewModel>
            {
                Elenco = utenti,
                TotaleElenco = totale
            };

            return listViewModel;
        }
        public void RevocaNegozio(ApplicationUser user)
        {

            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();


            if(user.Proprietario == 1 && negozio != null){

                try
                {
                    negozio.Revoca();
                    user.Revocato = 1;
                    user.IdRuolo = 0;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Revoca negozio riuscita.");
                }
                catch (System.Exception)
                {
                    negozio.Assegna();
                    user.Revocato = 0;
                    user.IdRuolo = negozio.NegozioId;
                    logger.LogWarning($"Revoca negozio fallita.");
                    throw;
                }
            }else{
                try
                {
                    user.Revocato = 1;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Revoca negozio riuscita.");
                }
                catch (System.Exception)
                {
                    user.Revocato = 0;
                    logger.LogWarning($"Revoca negozio fallita.");
                    throw;
                }


            }
            
            
            
        }

        public void RevocaCorriere(ApplicationUser user)
        {

            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            if(user.Proprietario == 1 && corriere != null){
                corriere.Revoca();
                var pacchi = dbContext.Pacco.Where(c => c.CorriereId == corriere.CorriereId).ToList();
                foreach (var pacco in pacchi)
                {
                    pacco.RevocaCorriere();
                }
                try
                {
                    user.Revocato = 1;
                    user.IdRuolo = 0;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Revoca corriere riuscita.");
                }
                catch (Exception e)
                {
                    corriere.Assegna();
                    user.Revocato = 0;
                    user.IdRuolo = corriere.CorriereId;
                    foreach (var pacco in pacchi)
                    {
                        pacco.SettaCorriere(corriere.CorriereId);

                    }
                    logger.LogWarning($"Revoca corriere fallita. Eccezione: {e}");
                    throw;
                }
            }else{
                user.Revocato = 1;
                try
                {
                    dbContext.SaveChanges();
                    logger.LogInformation($"Revoca corriere riuscita.");
                }
                catch (Exception e)
                {
                    user.Revocato = 0;
                    logger.LogWarning($"Revoca corriere fallita. Eccezione: {e}");
                    throw;
                }
            }


        }
        public void AssegnaNegozio(ApplicationUser user)
        {
            var negozio = dbContext.Negozi.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();

            if(user.Proprietario == 1 && negozio != null){
                negozio.Assegna();
                
                try
                {
                    user.Revocato = 0;
                    user.IdRuolo = negozio.NegozioId;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Riassegnamento negozio riuscito.");
                }
                catch (Exception)
                {
                    negozio.Revoca();
                    user.Revocato = 1;
                    user.IdRuolo = 0;
                    logger.LogInformation($"Riassegnamento negozio fallito.");
                    throw;
                }
            }else{
                
                try
                {
                    user.Revocato = 0;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Riassegnamento negozio riuscito.");
                }
                catch (Exception)
                {
                    user.Revocato = 1;
                    logger.LogInformation($"Riassegnamento negozio fallito.");
                    throw;
                }
            }
        }
        public void AssegnaCorriere(ApplicationUser user)
        {
            var corriere = dbContext.Corrieri.Where(n => n.ProprietarioId == user.Id).FirstOrDefault();
            if(user.Proprietario == 1 && corriere != null){
                corriere.Assegna();
                try
                {
                    user.Revocato = 0;
                    user.IdRuolo = corriere.CorriereId;
                    dbContext.SaveChanges();
                    logger.LogInformation($"Riassegnamento corriere riuscito.");
                }
                catch (System.Exception)
                {
                    corriere.Revoca();
                    user.Revocato = 1;
                    user.IdRuolo = 0;
                    logger.LogInformation($"Riassegnamento corriere fallito.");
                    throw;
                }
            }else{
                user.Revocato = 0;
                try
                {
                    dbContext.SaveChanges();
                    logger.LogInformation($"Riassegnamento corriere riuscito.");
                }
                catch (System.Exception)
                {
                    user.Revocato = 1;
                    logger.LogInformation($"Riassegnamento corriere fallito.");
                    throw;
                }
            }
        }

        public bool AggiungiToken(Negozio negozio, int token){
            
             try
            {   
                negozio.IncrementaToken(token);
                dbContext.SaveChanges();
                logger.LogInformation("Incremento token riuscito");
                return true;
            }
            catch (System.Exception)
            {
                negozio.DecrementaToken(token);
                logger.LogWarning("Incremento token fallito");
                return false;
                throw;
            }
        }
        

        public bool RimuoviToken(Negozio negozio, int token){
            try
            {   
                if(negozio.RimuoviToken(token)){
                    dbContext.SaveChanges();
                    logger.LogInformation("Decremento token riuscito");
                    return true;
                }else{
                    logger.LogWarning("Decremento token fallito");
                    return false;
                }
                
            }
            catch (System.Exception)
            {
                negozio.IncrementaToken(token);
                logger.LogWarning("Decremento token fallito");
                return false;
                throw;
            }
        }


        public async Task<int> AssegnaAsync(ApplicationUser user, IList<Claim> claims , string ruoloDaAssegnare, Claim roleClaim)
        {
            if (user == null)
            {
                logger.LogWarning($"{user.Email} non trovato ");  
                return 1;
            }
    
            if(claims.Count() > 1 && !claims[1].Value.ToString().ToLower().Equals(ruoloDaAssegnare.ToLower())){
        
                logger.LogWarning($"Ruolo attivo dell'utente: {claims[1].Value}, impossibile assegnare {ruoloDaAssegnare}");
                return 2;
            }

            if (claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value))
            {
                logger.LogWarning($"Ruolo gia' assegnato a {user.Email}");
                return 3;  
            }

            IdentityResult result = await userManager.AddClaimAsync(user, roleClaim);
            if (!result.Succeeded){
                logger.LogWarning($"Assegnazione ruolo a {user.Email} fallita, {result.Errors}");
                return  4;
            }

            if (ruoloDaAssegnare == nameof(Categoria.Commerciante))
            {
                AssegnaNegozio(user);
                logger.LogInformation("Negozio assegnato");
                return 7;
            }
            else if (ruoloDaAssegnare == nameof(Categoria.Corriere))
            {
                AssegnaCorriere(user);
                logger.LogInformation("Corriere assegnato");
                return 7;
            }

            return 4;
        }

        public async Task<int> RevocaAsync(ApplicationUser user, IList<Claim> claims , string ruoloDaAssegnare, Claim roleClaim)
        {
            
            if (user == null){
                logger.LogWarning($"{user.Email} non trovato");
                return 1;
            }

            if (!claims.Any(c => c.Type == roleClaim.Type && c.Value == roleClaim.Value)){  
                logger.LogWarning($"Ruolo non assegnato a {user.Email}");
                return 5;
            }
            
            IdentityResult result = await userManager.RemoveClaimAsync(user, roleClaim);
            if (!result.Succeeded){   
                logger.LogWarning($"Assegnamento del ruolo a {user.Email} fallito, {result.Errors}");
                return 4;
            }

            if (ruoloDaAssegnare == nameof(Categoria.Commerciante))
            {
                RevocaNegozio(user);
                logger.LogInformation("Negozio revocato");
                return 6;
            }
            else if (ruoloDaAssegnare == nameof(Categoria.Corriere))
            {
                RevocaCorriere(user);
                logger.LogInformation("Corriere revocato");
                return 6;
            }

           return 4;
            
        }
        


    }
}