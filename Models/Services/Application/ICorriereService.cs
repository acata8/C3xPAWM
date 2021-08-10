using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface ICorriereService
    {

        CorriereViewModel CreateCorriere(CorriereInputModel model);
        bool EditCorriere (CorriereInputModel model);
        CorriereInputModel GetCorriere(int id);
    }
}