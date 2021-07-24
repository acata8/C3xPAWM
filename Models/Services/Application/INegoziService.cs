using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface INegoziService
    {
        Task<List<NegozioViewModel>> GetNegoziAsync(string search);
        Task<List<string>> GetListaRegioniDistinct();        
        Task<List<string>> GetListaCittaDistinct();       
        Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta);
        Task<List<NegozioViewModel>> GetNegoziByRegioneAsync(string regione);
    }
}