using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace C3xPAWM.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName {get; set;}
        public string Ruolo {get; set;}
        public int Proprietario {get; set;}
        public virtual ICollection<Negozio> ProprietarioNegozi {get; set;}
        public virtual ICollection<Corriere> ProprietarioCorriere {get; set;}

        public virtual ICollection<Pacco> Pacchi {get; set;}

        
    }
}