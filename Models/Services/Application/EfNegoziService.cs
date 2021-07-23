using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace C3xPAWM.Models.Services.Application
{
    public class EfNegoziService : INegoziService
    {
        private readonly C3PAWMDbContext dbContext;

        public EfNegoziService(C3PAWMDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
      
        public async Task<List<NegozioViewModel>> getNegoziAsync()
        {
           
            var negozi = await dbContext.Negozio.Select(negozio => new NegozioViewModel {
                Nome = negozio.Nome,
                Telefono = negozio.Telefono,
                Tipologia = negozio.Tipologia,
                Categoria = negozio.Categoria,
                Indirizzo = negozio.Indirizzi.Select(indirizzo => new IndirizzoViewModel {
                    Citta = indirizzo.Citta,
                    Via = indirizzo.Via,
                    Provincia = indirizzo.Provincia
                }).ToList()
            }).ToListAsync();

            
            return negozi;
        }

    }
}