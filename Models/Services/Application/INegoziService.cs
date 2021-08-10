using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface INegoziService
    {
        NegozioViewModel CreateNegozi(NegozioCreateInputModel model);
        Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model);
        
        ListViewModel<PubblicitaViewModel> GetNegoziPubblicizzati(ElencoListInputModel input);
        NegozioEditInputModel GetNegozio(int id);

        bool EditNegozio(NegozioEditInputModel model);
        PubblicitaViewModel CreatePubblicita(PubblicitaInputModel model);
        PubblicitaInputModel GetNegozioPubblicita(int id);
    }
}