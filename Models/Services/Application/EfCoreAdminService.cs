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
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreAdminService : IAdminService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;
        private readonly UserManager<ApplicationUser> userManager;
        
        public EfCoreAdminService(C3PAWMDbContext dbContext, IOptionsMonitor<ElencoOptions> elencoOptions, UserManager<ApplicationUser> userManager)
        {
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
                       Ruolo =u.Ruolo,
                       Nome = u.FullName,
                       Proprietario = u.Proprietario,
                       Revocato = u.Revocato
                   }).OrderBy(c => c.Ruolo);

            queryLinq = queryLinq.Where(u => u.Email.Contains(model.Search.ToLower()));
             
            var totale = queryLinq.Count();

            List<UtenteViewModel> utenti = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
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

    }
}