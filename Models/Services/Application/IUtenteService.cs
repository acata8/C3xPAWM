using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface IUtenteService
    {
        
        UtenteViewModel CreateUtente(UtenteInputModel model);
        bool EditUtente (UtenteInputModel model);
        UtenteInputModel GetUtente(int id);

    }
}