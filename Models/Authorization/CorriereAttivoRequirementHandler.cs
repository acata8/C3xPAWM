using System;
using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Services.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace C3xPAWM.Models.Authorization
{
    public class CorriereAttivoRequirementHandler : AuthorizationHandler<CorriereAttivoRequirement>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly ICorriereService service;
        public CorriereAttivoRequirementHandler(IHttpContextAccessor accessor, ICorriereService service)
        {
            this.service = service; 
            this.accessor = accessor;

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CorriereAttivoRequirement requirement)
        {
            bool isAuthorized = false;

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int CorriereId = Convert.ToInt32(accessor.HttpContext.Request.RouteValues["id"]);

            string proprietario = await service.GetCorriereIDAsync(CorriereId);

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