using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace C3xPAWM.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {get; set;}

        public virtual ICollection<Negozio> ProprietarioNegozi {get; set;}
        public virtual ICollection<Corriere> ProprietarioCorriere {get; set;}
    }
}