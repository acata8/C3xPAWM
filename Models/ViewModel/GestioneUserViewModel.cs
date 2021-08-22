using System.Collections.Generic;
using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;

namespace C3xPAWM.Models.ViewModel
{
    public class GestioneUserViewModel
    {
        
        public UserRoleInputModel Input {get; set;}
        public IList<ApplicationUser> Amministratori {get; set;}

        public IList<ApplicationUser> Commercianti {get; set;}

        public IList<ApplicationUser> Corrieri {get; set;}

        public IList<ApplicationUser> Utenti {get; set;}

    }
}