using System;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace C3xPAWM.Models.Authorization
{
    public class CorriereAttivoRequirementHandler : AuthorizationHandler<CorriereAttivoRequirement>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ICorriereService service;
        private readonly ILogger<CorriereAttivoRequirementHandler> logger;
        public CorriereAttivoRequirementHandler(IHttpContextAccessor accessor, ICorriereService service, ILogger<CorriereAttivoRequirementHandler> logger)
        {
            this.logger = logger;
            this.service = service;
            this.accessor = accessor;

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CorriereAttivoRequirement requirement)
        {
            bool isAuthorized = false;

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int CorriereId = Convert.ToInt32(accessor.HttpContext.Request.RouteValues["id"]);
            var user = context.User.FindFirst(ClaimTypes.Email).Value;
            string proprietario = await service.GetCorriereIDAsync(CorriereId);

            isAuthorized = (userId == proprietario);

            if (isAuthorized)
            {
                context.Succeed(requirement);

            }
            else
            {
                logger.LogWarning($"{user} non autorizzato ad accedere al servizio corriere {CorriereId}");
                context.Fail();
            }
        }
    }
}