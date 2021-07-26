using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface INegoziService
    {
        Task<List<NegozioViewModel>> GetNegozi(ElencoListInputModel model);
        Task<List<NegozioViewModel>> ByTipologia(ElencoListInputModel input);
    }
}