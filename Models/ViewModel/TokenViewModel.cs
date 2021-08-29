using System.Collections.Generic;
using C3xPAWM.Models.Entities;

namespace C3xPAWM.Models.ViewModel
{
    public class TokenViewModel
    {
        public Negozio Negozio {get; set;}
        public List<ApplicationUser> Amministratori {get; set;}
    }
}