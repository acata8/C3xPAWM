using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Models.Authorization
{
    public class CreazioneAttivitaRequirementHandler : AuthorizationHandler<CreazioneAttivitaRequirement>
    {
        private readonly ILogger<CreazioneAttivitaRequirementHandler> logger;
        private readonly UserManager<ApplicationUser> userManager;
        public CreazioneAttivitaRequirementHandler(ILogger<CreazioneAttivitaRequirementHandler> logger, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.logger = logger;
            
        }
        public UserManager<ApplicationUser> UserManager { get; }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreazioneAttivitaRequirement requirement)
        {

            bool isAuthorized = false;


            if (context.User != null)
            {
                var proprietario = context.User.FindFirst("Proprietario").Value;
                var user = context.User.FindFirst(ClaimTypes.Email).Value;
                
                
                isAuthorized = (proprietario.Equals("0") && (context.User.IsInRole("Commerciante") || context.User.IsInRole("Corriere")));

                if (isAuthorized)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                    logger.LogWarning($"{user} respinto accesso a creazione attivita'");
                }
            }
            else
            {
                logger.LogWarning($"User non loggato. respinto accesso a creazione attivita'");
            }

        }
    }
}
