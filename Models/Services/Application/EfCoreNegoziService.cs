using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Options;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreNegoziService : INegoziService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;

        public EfCoreNegoziService(C3PAWMDbContext dbContext, IOptionsMonitor<ElencoOptions> negozioOptions)
        {
            this.elencoOptions = negozioOptions;
            this.dbContext = dbContext;

        }

        public async Task<ListViewModel<NegozioViewModel>> ByTipologia(ElencoListInputModel input)
        {

            IQueryable<Negozio> baseQuery = dbContext.Negozi;
            var y = input.Search.ToUpper();
            var x = Enum.Parse(typeof(Tipologia), y);
            
            IQueryable<NegozioViewModel> queryLinq = baseQuery
            .AsNoTracking()
            .Where(negozio => negozio.Tipologia.Equals(x))
            .Select(negozio => new NegozioViewModel
            {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Via = negozio.Via,
                Citta = negozio.Citta,
                Provincia = negozio.Provincia,
                Regione = negozio.Regione
            });
            
            var totale = queryLinq.Count();

            List<NegozioViewModel> negozi = await queryLinq
            .Skip(input.Offset)
            .Take(input.Limit)
            .ToListAsync();

            ListViewModel<NegozioViewModel> listViewModel = new ListViewModel<NegozioViewModel>{
                Elenco = negozi,
                TotaleElenco = totale
            };

            return listViewModel;
        }

        public async Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model)
        {
            
            IQueryable<Negozio> baseQuery = dbContext.Negozi;

            switch(model.OrderBy){
                case "Nome":
                    if(model.Ascending){
                        baseQuery = baseQuery.OrderBy(ordinamento => ordinamento.Nome);
                    }else
                        baseQuery = baseQuery.OrderByDescending(ordinamento => ordinamento.Nome);
                    break;
                
                case "Tipologia":
                    if(model.Ascending){
                        baseQuery = baseQuery.OrderBy(ordinamento => ordinamento.Tipologia);
                    }else
                        baseQuery = baseQuery.OrderByDescending(ordinamento => ordinamento.Tipologia);
                    break;

            }

            IQueryable<NegozioViewModel> queryLinq = baseQuery
            .AsNoTracking()
            .Where(negozio => negozio.Nome.ToUpper().Contains(model.Search.ToUpper()))
            .Select(negozio => new NegozioViewModel
            {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Via = negozio.Via,
                Citta = negozio.Citta,
                Provincia = negozio.Provincia,
                Regione = negozio.Regione
            });
            
            var totale = queryLinq.Count();

            List<NegozioViewModel> negozi = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
            .ToListAsync();

            ListViewModel<NegozioViewModel> listViewModel = new ListViewModel<NegozioViewModel>{
                Elenco = negozi,
                TotaleElenco = totale
            };

            return listViewModel;
        }

    }
}