using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public interface IUtenteService
    {
        Task<ListViewModel<PaccoViewModel>> GetOrdini(ElencoListInputModel model); 
    }
}