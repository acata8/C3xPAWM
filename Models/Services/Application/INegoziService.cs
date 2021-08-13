using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    
    public interface INegoziService
    {
        Task<NegozioViewModel> CreateNegoziAsync(NegozioCreateInputModel model);
        Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model);
        
        ListViewModel<PubblicitaViewModel> GetNegoziPubblicizzati(ElencoListInputModel input);
        NegozioEditInputModel GetNegozioEdit(int id);

        bool EditNegozio(NegozioEditInputModel model);
        PubblicitaViewModel CreatePubblicita(PubblicitaInputModel model);
        PubblicitaInputModel GetNegozioPubblicita(int id);
        Task<string> GetNegozioIdAsync(int negozioId);
        Negozio GetNegozio(int id);
        PaccoCreateInputModel GetNegozioPacco(int id);
        string getIndirizzo(int id);
        List<Pacco> GetPacchiNegozio(int id);

        /*
PaccoInputModel GetNegozioPacco(int id);

*/
        Task<bool> RicercaEmailAsync(string email);
        Task<ApplicationUser> GetUtenteAsync(string email);
        bool CreateOrder(PaccoCreateInputModel model);
    }
}