using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using System.Linq;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreCorrieriService : ICorriereService
    {
        private readonly C3PAWMDbContext dbContext;

        public EfCoreCorrieriService(C3PAWMDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public CorriereViewModel CreateCorriere(CorriereInputModel model)
        {
            var corriere = new Corriere(model.Email, model.Password, model.Nominativo, model.Telefono);
            dbContext.Add(corriere);
            dbContext.SaveChanges();

            return CorriereViewModel.FromEntity(corriere);
        }

        public CorriereInputModel GetCorriere(int id)
        {
            return dbContext.Corrieri.Where(n => n.CorriereId == id)
                    .Select(corriere => new CorriereInputModel
                    {
                        CorriereId = corriere.CorriereId,
                        Nominativo = corriere.Nominativo,
                        Telefono = corriere.Telefono,
                        Email =corriere.Email,
                        Password = corriere.Password
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

    }  
}
