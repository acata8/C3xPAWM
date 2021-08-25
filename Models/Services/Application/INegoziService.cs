using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    
    public interface INegoziService
    {
        Task<NegozioViewModel> CreateNegoziAsync(NegozioInputModel model);
        Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model, bool admin);
        
        ListViewModel<PubblicitaViewModel> GetNegoziPubblicizzati(ElencoListInputModel input);
        NegozioInputModel GetNegozioEdit(int id);

        bool EditNegozio(NegozioInputModel model);
        PubblicitaViewModel CreatePubblicita(PubblicitaInputModel model);
        PubblicitaInputModel GetNegozioPubblicita(int id);
        Task<string> GetNegozioIdAsync(int negozioId);
        Negozio GetNegozio(int id);
        string getIndirizzo(int id);
        List<PaccoViewModel> GetPacchiNegozio(int id);

        Task<bool> RicercaEmailAsync(string email);
        Task<ApplicationUser> GetUtenteAsync(string email);
        bool CreateOrder(PaccoCreateInputModel model);
        
    }
}