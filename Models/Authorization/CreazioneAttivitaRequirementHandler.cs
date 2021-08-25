using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace C3xPAWM.Models.Authorization
{
    public class CreazioneAttivitaRequirementHandler : AuthorizationHandler<CreazioneAttivitaRequirement>
    {
        public CreazioneAttivitaRequirementHandler()
        {
            
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreazioneAttivitaRequirement requirement)
        {
            bool isAuthorized = false;

            var proprietario = context.User.FindFirst("Proprietario").Value;
            
        
            isAuthorized = proprietario.Equals("0");
            
            if (isAuthorized)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
