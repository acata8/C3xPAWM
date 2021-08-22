using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Enums;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreCorrieriService : ICorriereService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IHttpContextAccessor accessor;
        private readonly UserManager<ApplicationUser> userManager;

        public EfCoreCorrieriService(C3PAWMDbContext dbContext, IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.accessor = accessor;
            this.dbContext = dbContext;

        }
        public async Task<CorriereViewModel> CreateCorriereAsync(CorriereInputModel model)
        {
            string proprietario;
            string proprietarioId;
            try
            {
                proprietario = accessor.HttpContext.User.FindFirst("FullName").Value;
                proprietarioId = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userActive = await userManager.GetUserAsync(accessor.HttpContext.User);
                userActive.Proprietario = 1;
                IdentityResult result = await userManager.UpdateAsync(userActive);
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

        public Task<string> GetCorriereIDAsync(int corriereId)
        {
            return dbContext.Corrieri
                    .Where(n => n.CorriereId == corriereId)
                    .Select(n => n.ProprietarioId)
                    .FirstOrDefaultAsync();
        }

        public Corriere GetCorriereID(int id)
        {
            return dbContext.Corrieri
                    .Where(n => n.CorriereId == id)
                    .FirstOrDefault();
        }

        public List<PaccoViewModel> GetPacchiCorriere(int id)
        {
            return dbContext.Pacco
                .Where(p => p.CorriereId == id)
                .Where(p => p.StatoPacco == StatoPacco.ASSEGNATO)
                .Include(p => p.Utente)
                .Include(p => p.Negozio)
                .Select(p => new PaccoViewModel{
                    PaccoId = p.PaccoId,
                    Negozio = p.Negozio,
                    Corriere = p.Corriere,
                    Utente = p.Utente,
                    Destinazione = p.Destinazione,
                    Partenza = p.Partenza,
                    StatoPacco = p.StatoPacco
                })
                .ToList();
        }


       
        public List<PaccoViewModel> GetPacchiNonAssegnati()
        {
           return dbContext.Pacco
                .Where(p => p.CorriereId == 6)
                .Where(p => p.StatoPacco == StatoPacco.NON_ASSEGNATO)
                .Include(p => p.Utente)
                .Include(p => p.Negozio)
                .Select(p => new PaccoViewModel{
                    PaccoId = p.PaccoId,
                    Negozio = p.Negozio,
                    Corriere = p.Corriere,
                    Utente = p.Utente,
                    Destinazione = p.Destinazione,
                    Partenza = p.Partenza,
                    StatoPacco = p.StatoPacco
                    
                })
                .ToList();
        }

        public bool AssegnaPacco(PaccoViewModel model)
        {
            if(model.CorriereId != 6){
                Corriere corriere = dbContext.Corrieri.Where(r => r.CorriereId == model.CorriereId).First();
                Pacco paccoScelto = dbContext.Pacco.Where(p => p.PaccoId == model.PaccoId).First();
                paccoScelto.SettaCorriere(corriere.CorriereId);
                try{
                    dbContext.SaveChanges();
                    return true;
                }catch{
                    return false;
                }   
            }

            return false;
        }

        public List<PaccoViewModel> GetCronologiaPacchi(int id)
        {
            return dbContext.Pacco
                .Where(p => p.CorriereId == id)
                .Where(p => p.StatoPacco == StatoPacco.CONSEGNATO)
                .Include(p => p.Utente)
                .Include(p => p.Negozio)
                .Select(p => new PaccoViewModel{
                    PaccoId = p.PaccoId,
                    Negozio = p.Negozio,
                    Corriere = p.Corriere,
                    Utente = p.Utente,
                    Destinazione = p.Destinazione,
                    Partenza = p.Partenza,
                    StatoPacco = p.StatoPacco,
                    Data = p.dataConsegna
                })
                .ToList();
        }

        public bool ConsegnaPacco(PaccoViewModel model)
        {
            if(model.CorriereId != 6){
                
                Pacco paccoScelto = dbContext.Pacco.Where(p => p.PaccoId == model.PaccoId).First();
                paccoScelto.SettaConsegnato();
                paccoScelto.dataConsegna = model.Data;
                try{
                    dbContext.SaveChanges();
                    return true;
                }catch{
                    return false;
                }   
            }

            return false;
        }
    }
}


