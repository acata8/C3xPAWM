using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface INegoziService
    {
        Task<List<NegozioViewModel>> GetNegoziAsync(string search, int page);   
        Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta, int page);
        Task<List<NegozioViewModel>> GetNegoziByProvinciaAsync(string regione);
    }
}