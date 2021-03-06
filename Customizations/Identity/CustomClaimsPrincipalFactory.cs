using System.Security.Claims;
using System.Threading.Tasks;
using C3xPAWM.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Customizations.Identity
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
        protected override  async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullName", user.FullName));
            identity.AddClaim(new Claim("Proprietario", user.Proprietario.ToString()));
            identity.AddClaim(new Claim("IdRuolo", user.IdRuolo.ToString()));
            identity.AddClaim(new Claim("Revocato", user.Revocato.ToString()));
            return identity;
        }
    }
}