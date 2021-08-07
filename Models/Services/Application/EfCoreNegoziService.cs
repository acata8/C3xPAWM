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

        public async Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model)
        {
            
            IQueryable<Negozio> baseQuery = dbContext.Negozi;

            var orderBy = model.OrderBy;
            var ascending = model.Ascending;
            var tipologia = model.Tipologia;
            var offset = model.Offset;
            var limit = model.Limit;

             switch(orderBy){
                case "Nome":
                    if(ascending){
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

            if(tipologia){
                var y = model.Search.ToUpper();
                Tipologia x;
                if (Enum.TryParse(y, true, out x))
                    queryLinq = queryLinq.Where(negozio => negozio.Tipologia == x);
                queryLinq = queryLinq.Where(negozio => negozio.Tipologia.Equals(x));
            }else{
                queryLinq = queryLinq.Where(negozio => negozio.Nome.ToUpper().Contains(model.Search.ToUpper()));
            }


            var totale = queryLinq.Count();

            List<NegozioViewModel> negozi = await queryLinq
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            ListViewModel<NegozioViewModel> listViewModel = new ListViewModel<NegozioViewModel>{
                Elenco = negozi,
                TotaleElenco = totale
            };

            return listViewModel;
        }

        public ListViewModel<PubblicitaViewModel> GetNegoziPubblicizzati(ElencoListInputModel input){
            
            IQueryable<Pubblicita> baseQuery =  dbContext.Pubblicita;

            var offset = input.Offset;
            var limit = input.Limit;
            var queryLinq = baseQuery
                        .AsNoTracking()
                        .Where(p => p.Attiva == 1)
                        .Select(p => new PubblicitaViewModel{
                            PubblicitaId = p.PubblicitaId,
                            NomeEvento = p.NomeEvento,
                            Durata = p.Durata,
                            Attiva = p.Attiva,
                            Negozio = p.Negozio
                        })
                        .ToList();

            var totale = queryLinq.Count();

            List<PubblicitaViewModel> negozi = queryLinq
                    .Skip(offset)
                    .Take(limit)
                    .ToList();

            ListViewModel<PubblicitaViewModel> listViewModel = new ListViewModel<PubblicitaViewModel>{
                Elenco = negozi,
                TotaleElenco = totale,
            };

            return listViewModel;
            
        }
            
    }
}

