using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.Services.Application
{
    public interface IAdminService
    {   

        Task<IList<ApplicationUser>> GetUtentiAsync(string ruolo);
    }
}