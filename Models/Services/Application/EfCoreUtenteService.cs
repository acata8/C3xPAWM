using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace C3xPAWM.Models.Services.Application
{
    [Authorize]
    public class EfCoreUtenteService : IUtenteService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IHttpContextAccessor accessor;
        public EfCoreUtenteService(C3PAWMDbContext dbContext, IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
            this.dbContext = dbContext;
        }
        
        
        public async Task<ListViewModel<PaccoViewModel>> GetOrdini(ElencoListInputModel model)
        {
            var baseQuery = dbContext.Pacco;

            var offset = model.Offset;
            var limit = model.Limit;

            IQueryable<PaccoViewModel> queryLinq = baseQuery
                   .AsNoTracking()
                   .Where(n => n.Utente.Email == accessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value)
                   .Select(p => new PaccoViewModel
                   {
                       Corriere = p.Corriere,
                       Data = p.dataConsegna,
                       Negozio = p.Negozio,
                       Destinazione = p.Destinazione
                   });

            var totale = queryLinq.Count();

            List<PaccoViewModel> pacchi = await queryLinq
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            ListViewModel<PaccoViewModel> listViewModel = new ListViewModel<PaccoViewModel>
            {
                Elenco = pacchi,
                TotaleElenco = totale
            };

            return listViewModel;
        }
    }
}