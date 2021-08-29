using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface IAdminService
    {   

        Task<ListViewModel<UtenteViewModel>> GetUtenteEmailAsync(ElencoListInputModel model);
        void AssegnaCorriere(ApplicationUser user);
        void AssegnaNegozio(ApplicationUser user);
        void RevocaCorriere(ApplicationUser user);
        void RevocaNegozio(ApplicationUser user);
        bool AggiungiToken(Negozio negozio, int token);
        bool RimuoviToken(Negozio negozio, int token);
        Task<int> RevocaAsync(ApplicationUser user, IList<Claim> claims , string ruoloDaAssegnare, Claim roleClaim);
        Task<int> AssegnaAsync(ApplicationUser user, IList<Claim> claims, string ruoloDaAssegnare, Claim roleClaim);
    }
}