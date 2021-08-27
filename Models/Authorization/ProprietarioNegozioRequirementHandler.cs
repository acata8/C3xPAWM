using System;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Models.Authorization
{
    public class ProprietarioNegozioRequirementHandler : AuthorizationHandler<ProprietarioNegozioRequirement>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly INegoziService service;
        private readonly ILogger<ProprietarioNegozioRequirementHandler> logger;
        public ProprietarioNegozioRequirementHandler(IHttpContextAccessor accessor, INegoziService service, ILogger<ProprietarioNegozioRequirementHandler> logger)
        {
            this.logger = logger;
            this.service = service;
            this.accessor = accessor;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProprietarioNegozioRequirement requirement)
        {
            bool isAuthorized = false;
            
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int NegozioId = 0;
            string proprietario = "";
            
            int.TryParse(accessor.HttpContext.Request.RouteValues["id"].ToString(), out NegozioId);
            proprietario = await service.GetNegozioIdAsync(NegozioId);
                    
            var user = context.User.FindFirst(ClaimTypes.Email).Value;
            
            isAuthorized = (userId == proprietario);

            if (isAuthorized)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                
            }
        }
    }
}