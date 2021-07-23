using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface INegoziService
    {
        Task<List<NegozioViewModel>> getNegoziAsync();
        //Task<List<NegozioViewModel>> getNegoziByCittaAsync(string citta);
        //Task<List<NegozioViewModel>> getListaCittaDistinct();
    }
}