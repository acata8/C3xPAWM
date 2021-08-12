using Microsoft.AspNetCore.Identity;

namespace C3xPAWM.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {get; set;}

        
    }
}