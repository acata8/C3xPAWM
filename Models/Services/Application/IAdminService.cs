using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface IAdminService
    {   

        Task<IList<ApplicationUser>> GetUtentiAsync(string ruolo);
        
        Task<ListViewModel<UtenteViewModel>> GetUtenteEmailAsync(ElencoListInputModel model);
        void AssegnaCorriere(ApplicationUser user);
        void AssegnaNegozio(ApplicationUser user);
        void RevocaCorriere(ApplicationUser user);
        void RevocaNegozio(ApplicationUser user);
    }
}