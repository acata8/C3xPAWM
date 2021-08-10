using System.Linq;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreUtentiService : IUtenteService
    {
        
        private readonly C3PAWMDbContext dbContext;

        public EfCoreUtentiService(C3PAWMDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public UtenteViewModel CreateUtente(UtenteInputModel model)
        {
            var utente = new Utente(model.Email, model.Password, model.Nome, model.Telefono);
            dbContext.Add(utente);
            dbContext.SaveChanges();

            return UtenteViewModel.FromEntity(utente);
        }

        public UtenteInputModel GetUtente(int id)
        {
            return dbContext.Utenti.Where(n => n.UtenteId == id)
                    .Select(utente => new UtenteInputModel
                    {
                        UtenteId = utente .UtenteId,
                        Nome = utente .Nome,
                        Telefono = utente .Telefono,
                        Email =utente .Email,
                        Password = utente .Password
                    }).FirstOrDefault();
        }
        
        public bool EditUtente(UtenteInputModel model)
        {
            Utente utente = dbContext.Utenti.Find(model.UtenteId);

            utente.CambiaNome(model.Nome);
            utente.CambiaTelefono(model.Telefono);
            utente.CambiaEmail(model.Email);
            utente.CambiaPassword(model.Password);
        
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

    }
}