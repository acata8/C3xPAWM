using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreCorrieriService : ICorriereService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IHttpContextAccessor accessor;

        public EfCoreCorrieriService(C3PAWMDbContext dbContext, IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
            this.dbContext = dbContext;

        }
        public CorriereViewModel CreateCorriere(CorriereInputModel model)
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

            var corriere = new Corriere(model.Nominativo, model.Telefono, proprietario, proprietarioId);
            dbContext.Add(corriere);
            try
            {
                dbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                
                throw;
            }
            

            return CorriereViewModel.FromEntity(corriere);
        }

        public CorriereInputModel GetCorriere(int id)
        {
            return dbContext.Corrieri.Where(n => n.CorriereId == id)
                    .Select(corriere => new CorriereInputModel
                    {
                        CorriereId = corriere.CorriereId,
                        Nominativo = corriere.Nominativo,
                        Telefono = corriere.Telefono
                    }).FirstOrDefault();
        }

        public bool EditCorriere(CorriereInputModel model)
        {
            Corriere corriere = dbContext.Corrieri.Find(model.CorriereId);

            corriere.CambiaNome(model.Nominativo);
            corriere.CambiaTelefono(model.Telefono);

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

        /*
        public ListViewModel<PaccoViewModel> GetPacchiNonAssegnati()
        {
           IQueryable<PaccoViewModel> queryLinq = dbContext.Pacchi
                    .AsNoTracking()
                    .Where(n => n.CorriereId == 1)
                    .Where(n => n.StatoPacco == StatoPacco.NON_ASSEGNATO)
                    .Select(p => new PaccoViewModel
                    {
                        PaccoId = p.PaccoId,
                        Utente = p.Utenti,
                        Negozio = p.Negozio,
                        Via = p.Via,
                        Citta = p.Citta,
                        Provincia = p.Provincia,
                        
                        StatoPacco = p.StatoPacco
                    });

            var totale = queryLinq.Count();

            List<PaccoViewModel> Pacco = queryLinq
            .ToList();

            ListViewModel<PaccoViewModel> listViewModel = new ListViewModel<PaccoViewModel>{
                Elenco = Pacco,
                TotaleElenco = totale
            };

            return listViewModel;
        }

        public bool AssegnaPacco(PaccoAssegnatoViewModel model)
        {
            if(model.CorriereId > 1){
                int paccoid = model.PaccoId;
                Corriere corriere = dbContext.Corrieri.Where(r => r.CorriereId == model.CorriereId).First();
                Pacco paccoScelto = dbContext.Pacchi.Where(p => p.PaccoId == paccoid).First();
                paccoScelto.SetCorriere(corriere);
                try{
                dbContext.SaveChanges();
                    return true;
                }catch{
                    return false;
                }   
            }

            return false;
            */
    }
}

