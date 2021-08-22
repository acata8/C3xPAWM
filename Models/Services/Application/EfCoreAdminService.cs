using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.Options;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class EfCoreAdminService : IAdminService
    {
        private readonly C3PAWMDbContext dbContext;
        private readonly IOptionsMonitor<ElencoOptions> elencoOptions;
        private readonly UserManager<ApplicationUser> userManager;
        
        public EfCoreAdminService(C3PAWMDbContext dbContext, IOptionsMonitor<ElencoOptions> elencoOptions, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.elencoOptions = elencoOptions;
            this.dbContext = dbContext;
        }

       

        public async Task<IList<ApplicationUser>> GetUtentiAsync(string ruolo)
        {
            Claim claim = new(ClaimTypes.Role, ruolo);

            IList<ApplicationUser> userInRole = await userManager.GetUsersForClaimAsync(claim);

            return userInRole;
        }




    }
}