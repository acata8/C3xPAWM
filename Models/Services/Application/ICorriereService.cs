using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface ICorriereService
    {

        
        Task<CorriereViewModel> CreateCorriereAsync(CorriereInputModel model);
        bool EditCorriere (CorriereInputModel model);
        CorriereInputModel GetCorriere(int id);
        Task<string> GetCorriereIDAsync(int corriereId);
        Corriere GetCorriereID(int id);
        List<Pacco> GetPacchiCorriere(int id);
        List<PaccoViewModel> GetPacchiNonAssegnati();
        bool AssegnaPacco(PaccoViewModel model);
    }
}