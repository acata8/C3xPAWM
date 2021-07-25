using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
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
        private readonly IOptionsMonitor<NegoziOptions> negozioOptions;

        public EfCoreNegoziService(C3PAWMDbContext dbContext, IOptionsMonitor<NegoziOptions> negozioOptions)
        {
            this.negozioOptions = negozioOptions;
            this.dbContext = dbContext;

        }

        public async Task<List<NegozioViewModel>> GetNegoziAsync(string search, int page)
        {
            search = search ?? "";

            page = Math.Max(1, page);
            int limit = negozioOptions.CurrentValue.PerPage;
            int offset = (page - 1) * limit;


            var negozi = await dbContext.Negozio
            .Skip(offset)
            .Take(limit)
            .AsNoTracking()
            .Where(negozio => negozio.Nome.Contains(search))
            .Select(negozio => new NegozioViewModel
            {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel
                {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Provincia = indirizzo.Provincia
                }).ToList()
            })
            .ToListAsync();

            return negozi;
        }

    
        public async Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta, int page)
        {

            citta = citta.ToUpper() ?? "";
            var negozi = await dbContext.Negozio
            .AsNoTracking()
            .Where(negozio => negozio.Indirizzi.First().Citta.ToUpper().Equals(citta))
            .Select(negozio => new NegozioViewModel
            {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel
                {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Provincia = indirizzo.Provincia
                }).ToList()
            })
            .ToListAsync();

            if (!negozi.Any()) throw new InvalidOperationException($"Impossibile, localita' non trovata");

            return negozi;

        }
        public async Task<List<NegozioViewModel>> GetNegoziByProvinciaAsync(string provincia)
        {

            provincia = provincia.ToUpper() ?? "";

            var negozi = await dbContext.Negozio
            .AsNoTracking()
            .Where(negozio => negozio.Indirizzi.First().Provincia.ToUpper().Equals(provincia))
            .Select(negozio => new NegozioViewModel
            {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel
                {
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