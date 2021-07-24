using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreNegoziService : INegoziService
    {
        private readonly C3PAWMDbContext dbContext;

        public EfCoreNegoziService(C3PAWMDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        public async Task<List<string>> GetListaCittaDistinct()
        {
            var citta = await dbContext.Negozio
            .AsNoTracking()
            .Select(negozio => negozio.Indirizzi.First().Citta)
            .Distinct()
            .ToListAsync();

            return citta;
        }

        public async Task<List<string>> GetListaRegioniDistinct()
        {
            var Regioni = await dbContext.Negozio
            .AsNoTracking()
            .Select(negozio => negozio.Indirizzi.First().Regione)
            .Distinct()
            .ToListAsync();
        
            return Regioni;
        }

        public async Task<List<NegozioViewModel>> GetNegoziAsync(string search)
        {
            search = search ?? "";
            var negozi = await dbContext.Negozio
            .AsNoTracking()
            .Where(negozio => negozio.Nome.Contains(search))
            .Select(negozio => new NegozioViewModel {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Provincia = indirizzo.Provincia
                }).ToList()
            })
            .ToListAsync();

            return negozi;
        }

        public async Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta)
        {
            var negozi = await dbContext.Negozio
            .AsNoTracking()
            .Where(negozio => negozio.Indirizzi.First().Citta.Equals(citta))
            .Select(negozio => new NegozioViewModel {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Provincia = indirizzo.Provincia
                }).ToList()
            })
            .ToListAsync();

            if (!negozi.Any()) throw new InvalidOperationException($"Impossibile, localita' non trovata");
            
            return negozi;
            
        }
        public async Task<List<NegozioViewModel>> GetNegoziByRegioneAsync(string regione)
        {
            var negozi = await dbContext.Negozio
            .AsNoTracking()
            .Where(negozio => negozio.Indirizzi.First().Regione.Equals(regione))
            .Select(negozio => new NegozioViewModel {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Regione = indirizzo.Regione
                }).ToList()
            })
            .ToListAsync();

            if (!negozi.Any()) throw new InvalidOperationException($"Impossibile, localita' non trovata");

            return negozi;
        }
    }
}