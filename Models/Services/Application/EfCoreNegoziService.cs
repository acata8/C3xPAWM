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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreNegoziService : INegoziService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;
        private readonly IHttpContextAccessor accessor;

        public EfCoreNegoziService(C3PAWMDbContext dbContext, IOptionsMonitor<ElencoOptions> negozioOptions, IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
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

            switch (orderBy)
            {
                case "Nome":
                    if (ascending)
                    {
                        baseQuery = baseQuery.OrderBy(ordinamento => ordinamento.Nome);
                    }
                    else
                        baseQuery = baseQuery.OrderByDescending(ordinamento => ordinamento.Nome);
                    break;

                case "Tipologia":
                    if (model.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(ordinamento => ordinamento.Tipologia);
                    }
                    else
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

            if (tipologia)
            {
                var y = model.Search.ToUpper();
                Tipologia x;
                if (Enum.TryParse(y, true, out x))
                    queryLinq = queryLinq.Where(negozio => negozio.Tipologia == x);

            }
            else
            {
                queryLinq = queryLinq.Where(negozio => negozio.Nome.ToUpper().Contains(model.Search.ToUpper()));
            }


            var totale = queryLinq.Count();

            List<NegozioViewModel> negozi = await queryLinq
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            ListViewModel<NegozioViewModel> listViewModel = new ListViewModel<NegozioViewModel>
            {
                Elenco = negozi,
                TotaleElenco = totale
            };

            return listViewModel;
        }

        public ListViewModel<PubblicitaViewModel> GetNegoziPubblicizzati(ElencoListInputModel input)
        {

            IQueryable<Pubblicita> baseQuery = dbContext.Pubblicita;

            var offset = input.Offset;
            var limit = input.Limit;
            var queryLinq = baseQuery
                        .AsNoTracking()
                        .Where(p => p.Attiva == 1)
                        .Select(p => new PubblicitaViewModel
                        {
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

            ListViewModel<PubblicitaViewModel> listViewModel = new ListViewModel<PubblicitaViewModel>
            {
                Elenco = negozi,
                TotaleElenco = totale,
            };

            return listViewModel;
        }

        public NegozioViewModel CreateNegozi(NegozioCreateInputModel model)
        {
            string proprietario;
            string proprietarioId;
            try
            {
                proprietario = accessor.HttpContext.User.FindFirst("FullName").Value;
                proprietarioId = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (NullReferenceException)
            {
                
                throw;
            }
             
            var negozio = new Negozio(model.Nome, model.Telefono, model.Provincia.ToUpper(), model.Regione,
             model.Citta, model.Via, model.Tipologia, proprietario, proprietarioId);
            dbContext.Add(negozio);
            try
            {
                dbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
            

            return NegozioViewModel.FromEntity(negozio);
        }
        

        public NegozioEditInputModel GetNegozio(int id)
        {
            return dbContext.Negozi.Where(n => n.NegozioId == id)
                    .Select(negozio => new NegozioEditInputModel
                    {
                        NegozioId = negozio.NegozioId,
                        Nome = negozio.Nome,
                        Telefono = negozio.Telefono,
                        Via = negozio.Via,
                        Citta = negozio.Citta,
                        Provincia = negozio.Provincia,
                        Regione = negozio.Regione,
                        Tipologia = negozio.Tipologia
                    }).FirstOrDefault();
        }

        public bool EditNegozio(NegozioEditInputModel model)
        {
            Negozio negozio = dbContext.Negozi.Find(model.NegozioId);

            negozio.CambiaNome(model.Nome);
            negozio.CambiaTelefono(model.Telefono);
            negozio.CambiaCitta(model.Citta);
            negozio.CambiaProvincia(model.Provincia);
            negozio.CambiaVia(model.Via);
            negozio.CambiaRegione(model.Regione);
            negozio.settaTipologia(Convert.ToString(model.Tipologia));
            
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public PubblicitaInputModel GetNegozioPubblicita(int id)
        {
            return dbContext.Negozi.Where(n => n.NegozioId == id)
                    .Select(p => new PubblicitaInputModel
                    {
                        NegozioId = p.NegozioId,
                        Negozio = p
                    }).FirstOrDefault();
        }

        public PubblicitaViewModel CreatePubblicita(PubblicitaInputModel model)
        {
            var negozio = dbContext.Negozi.Where(n => n.NegozioId == model.NegozioId).Include(p => p.Pubblicita).FirstOrDefault();

            if (negozio.Token >= model.Durata && model.Durata > 0)
            {
                model.Attiva = 1;
                negozio.DecrementaToken(model.Durata);
            }
            else
            {
                model.Attiva = 0;
                throw new ArgumentException();
            }

            var pubblicita = new Pubblicita(negozio, model.NomeEvento, model.Durata);

            dbContext.Add(pubblicita);
            dbContext.SaveChanges();

            return PubblicitaViewModel.FromEntity(pubblicita);
        }

        /*
        public void CreateOrder(PaccoInputModel model)
        {
            Utente utente = dbContext.Utenti.Where(m => m.Email == model.Email).FirstOrDefault();
            var negozio = dbContext.Negozi.Find(model.NegozioId);
            model.Utente = utente;
            model.Negozio = negozio;


            var pacco = new Pacco(model.Provincia.ToUpper(), model.Via, model.Citta, utente.UtenteId, model.NegozioId);

            dbContext.Add(pacco);
            dbContext.SaveChanges();

        }

        public PaccoInputModel GetNegozioPacco(int id)
        {
            return dbContext.Negozi.Where(n => n.NegozioId == id)
                    .Select(p => new PaccoInputModel
                    {
                        NegozioId = p.NegozioId,
                        Negozio = p
                    }).FirstOrDefault();
        }

        public async Task<bool> RicercaEmailAsync(string email)
        {
            bool emailEsistente = await dbContext.Utenti.AnyAsync(c => EF.Functions.Like(c.Email, email));
            return emailEsistente;
        }
        */
    }
}
